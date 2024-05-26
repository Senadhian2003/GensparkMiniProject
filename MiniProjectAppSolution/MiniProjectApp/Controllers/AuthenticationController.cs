using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.BussinessLogics.Services;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;

namespace MiniProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthService _authBL;
       
        public AuthenticationController(IAuthService authBl)
        {
            _authBL = authBl;
        }
        [HttpPost("Login")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<User>> Login(UserLoginDTO userLoginDTO)
        {
            try
            {

                var result = await _authBL.Login(userLoginDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                
                return Unauthorized(new ErrorModel(401, ex.Message));
            }
        }
        [HttpPost("Register")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Register(UserRegisterDTO registerDTO)
        {
            try
            {
                User result = await _authBL.Register(registerDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }

    }
}
