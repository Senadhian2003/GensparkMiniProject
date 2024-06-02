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
    public class SaleController : ControllerBase
    {
        private readonly ISaleServices _saleServices;

        public SaleController(ISaleServices saleServices)
        {
            _saleServices = saleServices;

        }

        [HttpGet("ViewOrders")]
        [ProducesResponseType(typeof(Sale), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnCartDTO>> ViewOrders(int userId)
        {
            try
            {
                var orders = await _saleServices.ViewOrders(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


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

    }
}
