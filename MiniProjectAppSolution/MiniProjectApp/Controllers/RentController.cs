using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.BussinessLogics;
using MiniProjectApp.BussinessLogics.Interfaces;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;
using MiniProjectApp.Services.Interfaces;

namespace MiniProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {

        private readonly IRentServices _rentServices;

        public RentController(IRentServices rentServices)
        {

          _rentServices = rentServices;

        }



        [HttpPost("RentBooksToUser")]
        [ProducesResponseType(typeof(ReturnRentBooksDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> RentBooksToUser(RentBooksDTO dto)
        {
            try
            {
                ReturnRentBooksDTO res = await _rentServices.AddBooksToRent(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


        [HttpPost("ReturnRentedBooks")]
        [ProducesResponseType(typeof(ReturnRentedBooksCountDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnRentedBooksCountDTO>> ReturnRentedBooks(RentBooksDTO dto)
        {
            try
            {
                ReturnRentedBooksCountDTO res = await _rentServices.ReturnRentedBooks(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


    }
}
