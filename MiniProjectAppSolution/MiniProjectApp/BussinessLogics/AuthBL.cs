using MiniProjectApp.BussinessLogics.Services;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Repositories.Interface;
using System.Security.Cryptography;
using System.Text;

namespace MiniProjectApp.BussinessLogics
{
    public class AuthBL : IAuthService
    {
        private readonly IRepository<int, User> _userRepo;
        private readonly IRepository<int, UserCredential> _userCredentialRepo;
        private readonly ITokenService _tokenService;

        public AuthBL(IRepository<int, User> userRepo, IRepository<int, UserCredential> userCredentialRepo, ITokenService tokenService)
        {   
            _userRepo = userRepo;
            _userCredentialRepo = userCredentialRepo;
            _tokenService = tokenService;

        }


        public async Task<LoginReturnDTO> Login(UserLoginDTO loginDTO)
        {
            var userCredential = await _userCredentialRepo.GetByKey(loginDTO.UserId);
            if (userCredential == null)
            {
                throw new UnauthorizedUserException("Invalid username or password");
            }
            HMACSHA512 hMACSHA = new HMACSHA512(userCredential.HashKey);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isPasswordSame = ComparePassword(encrypterPass, userCredential.Password);
            if (isPasswordSame)
            {
                var user = await _userRepo.GetByKey(loginDTO.UserId);
                //if (userDB.Status == "Active")
                //    return employee;
                LoginReturnDTO loginReturnDTO = MapEmployeeToLoginReturnDTO(user);
                return loginReturnDTO;
                //throw new UserNotActiveException("Your account is not activated");
            }
            throw new UnauthorizedUserException("Invalid username or password");


        }

        private LoginReturnDTO MapEmployeeToLoginReturnDTO(User user)
        {
            LoginReturnDTO returnDTO = new LoginReturnDTO();
            returnDTO.UserID = user.Id;
            returnDTO.Role = user.Role ?? "User";
            returnDTO.Token = _tokenService.GenerateToken(user);
            return returnDTO;
        }


        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }




        public async Task<User> Register(UserRegisterDTO registerDTO)
        {
            User user = null;
            UserCredential userCredential = null;
            try
            {
                user = MapRegisterDTOToUser(registerDTO);
                userCredential = MapRegisterDTOToUserCredential(registerDTO);
                user = await _userRepo.Add(user);
                userCredential.UserId = user.Id;
                userCredential = await _userCredentialRepo.Add(userCredential);
                
                return user;
            }
            catch (Exception) { }
            if (user != null)
                await RevertUserInsert(user);
            if (userCredential != null && user == null)
                await RevertUserCredentialInsert(userCredential);
            throw new UnableToRegisterException("Not able to register at this moment");
        }

        private async Task RevertUserCredentialInsert(UserCredential userCredential)
        {
            await _userCredentialRepo.DeleteByKey(userCredential.UserId);
        }

        private async Task RevertUserInsert(User user)
        {
            await _userRepo.DeleteByKey(user.Id);
        }



        private User MapRegisterDTOToUser(UserRegisterDTO registerDTO)
        {
            User user = new User();
          
            user.Name = registerDTO.Name;
            user.Role = registerDTO.Role;
            user.Phone = registerDTO.Phone;
           
            return user;
        }


        private UserCredential MapRegisterDTOToUserCredential(UserRegisterDTO registerDTO)
        {
            UserCredential userCredential = new UserCredential();
           
            userCredential.Status = "Disabled";
            HMACSHA512 hMACSHA = new HMACSHA512();
            userCredential.HashKey = hMACSHA.Key;
            userCredential.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
            return userCredential;
        }




    }
}
