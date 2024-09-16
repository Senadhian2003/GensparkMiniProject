using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.BussinessLogics.Services;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using System.Diagnostics.CodeAnalysis;

namespace MiniProjectApp.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
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
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> Login(UserLoginDTO userLoginDTO)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new ErrorModel(400, string.Join("; ", errors)));
                }

                var result = await _authBL.Login(userLoginDTO);
                return Ok(result);
            }
            catch (UnauthorizedUserException uue)
            {
                
                return Unauthorized(new ErrorModel(401, uue.Message));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message)); ;
            }

        }


        [HttpPost("Register")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Register(UserRegisterDTO registerDTO)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new ErrorModel(400, string.Join("; ", errors)));
                }



                User result = await _authBL.Register(registerDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }

        //[Authorize(Roles = "User,Admin")]
        [HttpPut("UpgradeToPremium")]
        [ProducesResponseType(typeof(PremiumUserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PremiumUserDTO>> UpgrateToPremium(int userId)
        {
            try
            {
                var result = await _authBL.UpgradeToPremium(userId);
                return Ok(result);
            }
            catch (ElementNotFoundException enfe)
            {
                return BadRequest(new ErrorModel(404, enfe.Message));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message)); ;
            }

        }





    }
}
