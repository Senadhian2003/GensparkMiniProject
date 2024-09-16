using MiniProjectApp.Exceptions;
using MiniProjectApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagemenTest
{
    public class AuthServicesTest : BaseSetup
    {

        [Test]
        public async Task UserLogin()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO();
            userRegisterDTO.Name = "Spidey";
            userRegisterDTO.Role = "User";
            userRegisterDTO.Phone = "1234567890";
            userRegisterDTO.Password = "string";

            await _authServices.Register(userRegisterDTO);


            UserLoginDTO loginDTO = new UserLoginDTO();

            loginDTO.UserId = 4;
            loginDTO.Password = "string";

            var result = await _authServices.Login(loginDTO);

            Assert.IsNotNull(result);
            Assert.Pass();


        }

        [Test]
        public async Task UserLoginIncorrectUserId()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO();
            userRegisterDTO.Name = "Spidey";
            userRegisterDTO.Role = "User";
            userRegisterDTO.Phone = "1234567890";
            userRegisterDTO.Password = "string";

            await _authServices.Register(userRegisterDTO);


            UserLoginDTO loginDTO = new UserLoginDTO();

            loginDTO.UserId = 5;
            loginDTO.Password = "string";

           
            var exception = Assert.ThrowsAsync<UnauthorizedUserException>(async () => await _authServices.Login(loginDTO));
            Assert.That(exception.Message, Is.EqualTo("Invalid username or password"));


        }

        [Test]
        public async Task UserLoginIncorrectPassword()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO();
            userRegisterDTO.Name = "Spidey";
            userRegisterDTO.Role = "User";
            userRegisterDTO.Phone = "1234567890";
            userRegisterDTO.Password = "string";

            await _authServices.Register(userRegisterDTO);


            UserLoginDTO loginDTO = new UserLoginDTO();

            loginDTO.UserId = 4;
            loginDTO.Password = "stri";


            var exception = Assert.ThrowsAsync<UnauthorizedUserException>(async () => await _authServices.Login(loginDTO));
            Assert.That(exception.Message, Is.EqualTo("Invalid username or password"));


        }




        [Test]
        public async Task UserRegister()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO();
            userRegisterDTO.Name = "Spidey";
            userRegisterDTO.Role = "User";
            userRegisterDTO.Phone = "1234567890";
            userRegisterDTO.Password = "string";

            var result =  await _authServices.Register(userRegisterDTO);

            Assert.IsNotNull(result);
            Assert.Pass();


        }

        


        [Test]
        public async Task UpgradeToPremium()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO();
            userRegisterDTO.Name = "Spidey";
            userRegisterDTO.Role = "User";
            userRegisterDTO.Phone = "1234567890";
            userRegisterDTO.Password = "string";

            await _authServices.Register(userRegisterDTO);


            var result = await _authServices.UpgradeToPremium(4);

            Assert.That(result.Role, Is.EqualTo("Premium User"));
            Assert.Pass();

           


        }




    }
}
