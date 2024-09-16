using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;
using MiniProjectApp.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace MiniProjectApp.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleServices _saleServices;

        public SaleController(ISaleServices saleServices)
        {
            _saleServices = saleServices;

        }

        //[Authorize(Roles = "User,Premium User,Admin")]
        [HttpGet("ViewOrders")]
        [ProducesResponseType(typeof(Sale), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnCartDTO>> ViewOrders()
        {
            try
            {
                var userstring = User.Claims?.FirstOrDefault(x => x.Type == "Id")?.Value;
                var userId = Convert.ToInt32(userstring);
                var orders = await _saleServices.ViewOrders(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }

        //[Authorize(Roles = "User,Premium User,Admin")]
        [HttpGet("ViewOrderDetails")]
        [ProducesResponseType(typeof(List<SaleDetail>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<SaleDetail>>> ViewOrderDetails(int saleId)
        {
            try
            {
                var saleDetails = await _saleServices.ViewOrderDetail(saleId);
                return Ok(saleDetails);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


        //[Authorize(Roles = "User,Premium User,Admin")]
        [HttpGet("ViewRents")]
        [ProducesResponseType(typeof(Rent), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnCartDTO>> ViewRents(int userId)
        {
            try
            {
                
                var orders = await _saleServices.ViewRents(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }

        //[Authorize(Roles = "User,Premium User,Admin")]
        [HttpGet("ViewAllRents")]
        [ProducesResponseType(typeof(Rent), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnCartDTO>> ViewAllRents()
        {
            try
            {

                var rents = await _saleServices.ViewAllRents();
                return Ok(rents);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


        //[Authorize(Roles = "User,Premium User,Admin")]
        [HttpGet("ViewRentDetails")]
        [ProducesResponseType(typeof(List<SaleDetail>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<SaleDetail>>> ViewRentDetails(int rentIId)
        {
            try
            {
                var saleDetails = await _saleServices.ViewRentDetail(rentIId);
                return Ok(saleDetails);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }




    }
}
