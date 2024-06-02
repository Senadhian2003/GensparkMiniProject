using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.BussinessLogics;
using MiniProjectApp.BussinessLogics.Interfaces;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace MiniProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices)
        {
          
            _adminServices = adminServices;

        }


        





        //[HttpPost("ValidateUser")]
        //[ProducesResponseType(typeof(UserStatusDTO), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<List<Fine>>> ValidateUser(int UserId)
        //{
        //    try
        //    {
        //        await _adminServices.VerifyDue(UserId);
        //        UserStatusDTO result =  await _adminServices.VerifyUserPaidFine(UserId);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(new ErrorModel(404, ex.Message));
        //    }


        //}




    }
}
