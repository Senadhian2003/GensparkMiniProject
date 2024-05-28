using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.BussinessLogics;
using MiniProjectApp.BussinessLogics.Interfaces;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;

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


        [HttpPost("BuyBooksForLibrary")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> PurchaseBooks(PurchaseBooksForLibraryDTO dto)
        {
            try
            {
                int purchaseId = await _adminServices.PurchaseBooksForLibrary(dto);
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
                var purchases = await _adminServices.ViewPurchase();
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
                var purchase = await _adminServices.ViewPurchaseDetails(purchaseId);
                return Ok(purchase);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }



        [HttpPost("RentBooksToUser")]
        [ProducesResponseType(typeof(ReturnRentBooksDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> RentBooksToUser(RentBooksDTO dto)
        {
            try
            {
                ReturnRentBooksDTO res = await _adminServices.AddBooksToRent(dto);
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
                ReturnRentedBooksCountDTO res = await _adminServices.ReturnRentedBooks(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }



    }
}
