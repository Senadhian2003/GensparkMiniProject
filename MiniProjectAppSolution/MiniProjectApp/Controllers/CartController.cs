using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Models;
using MiniProjectApp.Services.Interfaces;

namespace MiniProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly ICartServices _cartServices;


        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }


        [HttpGet("ViewCartItems")]
        [ProducesResponseType(typeof(ViewCartDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ViewCartDTO>> ViewCartItems(int userId)
        {
            try
            {
                var cartItems = await _cartServices.GetCartItems(userId);
                return Ok(cartItems);

            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


        [HttpGet("ViewRentCartItems")]
        [ProducesResponseType(typeof(List<RentCart>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<RentCart>>> ViewRentCartItems(int userId)
        {
            try
            {
                var rentCartItems = await _cartServices.GetRentCartItems(userId);
                return Ok(rentCartItems);

            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }

        [HttpGet("ViewSuperRentCartItems")]
        [ProducesResponseType(typeof(List<SuperRentCart>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<SuperRentCart>>> ViewSuperRentCartItems(int userId)
        {
            try
            {
                var superRentCartItems = await _cartServices.GetSuperRentCartItems(userId);
                return Ok(superRentCartItems);

            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }





        [HttpPost("CheckoutCart")]
        [ProducesResponseType(typeof(Sale), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> ChecckoutCart(CheckoutCartDTO dto)
        {
            try
            {
                var sale = await _cartServices.CheckoutCart(dto.UserId);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }



        [HttpPost("AddItemToCart")]
        [ProducesResponseType(typeof(ReturnCartDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnCartDTO>> AddItemToCart(AddToCartDTO addToCartDTO)
        {
            try
            {
                Cart cartItem = await _cartServices.AddItemToCart(addToCartDTO.UserId, addToCartDTO.BookId, addToCartDTO.Quantity);
                ReturnCartDTO result = new ReturnCartDTO();

                result.UserId = cartItem.UserId;
                result.BookId = cartItem.BookId;
                result.Quantity = cartItem.Quantity;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }

        [HttpPost("DeleteItemFromCart")]
        [ProducesResponseType(typeof(ReturnCartDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnCartDTO>> DeleteItemFromCart(DeleteItemFromCartDTO deleteItemFromCartDTO)
        {
            try
            {
                Cart cartItem = await _cartServices.RemoveItemFromCart(deleteItemFromCartDTO.UserId, deleteItemFromCartDTO.BookId);
                return Ok(cartItem);

            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }



    }
}
