using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace MiniProjectApp.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class FineController : ControllerBase
    {

        private readonly IFineServices _fineServices;

        public FineController(IFineServices fineServices)
        {

            _fineServices = fineServices;

        }

        //[Authorize(Roles = "User,Premium User,Admin")]
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


        //[Authorize(Roles = "User,Premium User,Admin")]
        [HttpGet("ViewAllFines")]
        [ProducesResponseType(typeof(List<Fine>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Fine>>> ViewAllFines()
        {
            try
            {
                List<Fine> res = await _fineServices.ViewAllFines();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }

        //[Authorize(Roles = "User,Premium User,Admin")]
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


        //[Authorize(Roles = "User,Premium User,Admin")]
        [HttpPost("PayFine")]
        [ProducesResponseType(typeof(List<Fine>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Fine>>> PayFine(int FineId, int UserId)
        {
            try
            {
                Fine result = await _fineServices.PayFine(FineId, UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }

        //[Authorize(Roles = "User,Premium User,Admin")]
        [HttpPost("PayFineForOneBook")]
        [ProducesResponseType(typeof(List<Fine>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Fine>>> PayFineForOneBook(PayFineForOneBookDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorModel(400, string.Join("; ", errors)));
            }
            try
            {
                FineDetail result = await _fineServices.PayFineForOneBook(dto.FineId, dto.BookId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


    }
}
