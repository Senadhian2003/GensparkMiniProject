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
    public class CartController : ControllerBase
    {

        private readonly ICartServices _cartServices;


        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }

        //[Authorize(Roles = "User,Premium User")]
        [HttpGet("ViewCartItems")]
        [ProducesResponseType(typeof(ViewCartDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ViewCartDTO>> ViewCartItems()
        {
            try
            {
                var userstring = User.Claims?.FirstOrDefault(x => x.Type == "Id")?.Value;
                var userId = Convert.ToInt32(userstring);
                var cartItems = await _cartServices.GetCartItems(userId);
                return Ok(cartItems);

            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }

        //[Authorize(Roles = "User,Premium User,Admin")]
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


        //[Authorize(Roles = "User,Premium User,Admin")]
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




        //[Authorize(Roles = "User,Premium User")]
        [HttpPost("CheckoutCart")]
        [ProducesResponseType(typeof(Sale), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> ChecckoutCart()
        {
            try
            {
                var userstring = User.Claims?.FirstOrDefault(x => x.Type == "Id")?.Value;
                var userId = Convert.ToInt32(userstring);
                var sale = await _cartServices.CheckoutCart(userId);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


        [Authorize(Roles = "User,Premium User")]
        [HttpPost("AddItemToCart")]
        [ProducesResponseType(typeof(ReturnCartDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnCartDTO>> AddItemToCart(AddToCartDTO addToCartDTO)
        {
            try
            {
                var userstring = User.Claims?.FirstOrDefault(x => x.Type == "Id")?.Value;
                var userId = Convert.ToInt32(userstring);

                Cart cartItem = await _cartServices.AddItemToCart(userId, addToCartDTO.BookId, addToCartDTO.Quantity);
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

        //[Authorize(Roles = "User,Premium User")]
        [HttpDelete("DeleteItemFromCart")]
        [ProducesResponseType(typeof(ReturnCartDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnCartDTO>> DeleteItemFromCart(DeleteItemFromCartDTO deleteItemFromCartDTO)
        {
            try
            {
                var userstring = User.Claims?.FirstOrDefault(x => x.Type == "Id")?.Value;
                var userId = Convert.ToInt32(userstring);
                Cart cartItem = await _cartServices.RemoveItemFromCart(userId, deleteItemFromCartDTO.BookId);
                return Ok(cartItem);

            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }



    }
}
