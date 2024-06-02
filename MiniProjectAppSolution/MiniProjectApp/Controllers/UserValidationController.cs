using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.BussinessLogics;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;
using MiniProjectApp.Services.Interfaces;

namespace MiniProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserValidationController : ControllerBase
    {
        private readonly IUserValidationService _userValidationService;

        public UserValidationController(IUserValidationService userValidationService)
        {
            _userValidationService = userValidationService;
        }


        [HttpPost("ValidateUser")]
        [ProducesResponseType(typeof(UserStatusDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Fine>>> ValidateUser(int UserId)
        {
            try
            {
                await _userValidationService.VerifyDue(UserId);
                UserStatusDTO result = await _userValidationService.VerifyUserPaidFine(UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


    }
}
