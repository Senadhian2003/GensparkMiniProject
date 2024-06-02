using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.Models;
using MiniProjectApp.Services.Interfaces;

namespace MiniProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FineController : ControllerBase
    {

        private readonly IFineServices _fineServices;

        public FineController(IFineServices fineServices)
        {

            _fineServices = fineServices;

        }


        [HttpGet("ViewFines")]
        [ProducesResponseType(typeof(List<Fine>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Fine>>> ViewFines(int userId)
        {
            try
            {
                List<Fine> res = await _fineServices.ViewFines(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }

        [HttpGet("ViewUnpaidFines")]
        [ProducesResponseType(typeof(List<Fine>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Fine>>> ViewUnpaidFines(int userId)
        {
            try
            {
                List<Fine> res = await _fineServices.ViewUnPaidFines(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }

        [HttpPost("PayFine")]
        [ProducesResponseType(typeof(List<Fine>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Fine>>> PayFine(int RentId, int UserId)
        {
            try
            {
                Fine result = await _fineServices.PayFine(RentId, UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


    }
}
