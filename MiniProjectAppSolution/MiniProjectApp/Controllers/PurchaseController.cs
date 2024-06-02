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
    public class PurchaseController : ControllerBase
    {

        private readonly IPurchaseServices _purchaseServices;

        public PurchaseController(IPurchaseServices purchaseServices)
        {

            _purchaseServices = purchaseServices;

        }

        [HttpPost("BuyBooksForLibrary")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> PurchaseBooks(PurchaseBooksForLibraryDTO dto)
        {
            try
            {
                int purchaseId = await _purchaseServices.PurchaseBooksForLibrary(dto);
                return Ok(purchaseId);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


        [HttpPost("ViewPurchases")]
        [ProducesResponseType(typeof(List<Purchase>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> ViewPurchases()
        {
            try
            {
                var purchases = await _purchaseServices.ViewPurchase();
                return Ok(purchases);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }

        [HttpPost("ViewPurchaseDetails")]
        [ProducesResponseType(typeof(Purchase), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Purchase>> ViewPurchaseDetails(int purchaseId)
        {
            try
            {
                var purchase = await _purchaseServices.ViewPurchaseDetails(purchaseId);
                return Ok(purchase);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


    }
}
