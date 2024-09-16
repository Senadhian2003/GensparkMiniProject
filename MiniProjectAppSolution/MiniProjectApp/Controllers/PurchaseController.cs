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
    public class PurchaseController : ControllerBase
    {

        private readonly IPurchaseServices _purchaseServices;

        public PurchaseController(IPurchaseServices purchaseServices)
        {

            _purchaseServices = purchaseServices;

        }


        //[Authorize(Roles = "Admin")]
        [HttpPost("BuyBooksForLibrary")]
        [ProducesResponseType(typeof(Purchase), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> PurchaseBooks(PurchaseBooksForLibraryDTO dto)
        {
            try
            {
                var purchase = await _purchaseServices.PurchaseBooksForLibrary(dto);
                return Ok(purchase);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("ViewPurchases")]
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

        //[Authorize(Roles = "Admin")]
        [HttpGet("ViewPurchaseDetails")]
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
