using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.BussinessLogics.Interfaces;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<int, User> _userRepository;
        private readonly ICompositeKeyRepository<int, Cart> _superCartRepository;
        private readonly IUserServices _userServices;
        
        public UserController(IUserServices userServices)
        {
            //_userRepository = userRepository;
            //_superCartRepository = superCartRepository;
            _userServices = userServices;
          
        }

        //[HttpGet]
        //public async Task<ActionResult<DummyDTO>> Login(int id)
        //{
        //    User user = await _userRepository.GetByKey(id);

        //    var cartItems = user.SuperCartItems.ToList();

        //    foreach (var cartItem in cartItems)
        //    {
        //        Console.WriteLine("Book Id" + cartItem.BookId);
        //    }

        //    DummyDTO dto = new DummyDTO();

        //    dto.UserId = user.Id;
        //    dto.UserName = user.Name;

        //    return dto;


        //}


        //[HttpGet]
        //[Route("/DeleteCartItem")]
        //public async Task<ActionResult<Cart>> DeleteCartItem(int userId, int BookId)
        //{
        //    Cart Item = await _superCartRepository.DeleteByKey(userId, BookId);



        //    return Item;


        //}

        [HttpGet("ViewBooksForSale")]
        [ProducesResponseType(typeof(List<SalesStock>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<SalesStock>>> DeleteItemFromCart()
        {
            try
            {
                var salesItems = await _userServices.GetCurrentSaleBooks();
                return Ok(salesItems);

            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


        [HttpGet("ViewCartItems")]
        [ProducesResponseType(typeof(ViewCartDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ViewCartDTO>> ViewCartItems(int userId)
        {
            try
            {
                var cartItems = await _userServices.GetCartItems(userId);
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
                var rentCartItems = await _userServices.GetRentCartItems(userId);
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
                var superRentCartItems = await _userServices.GetSuperRentCartItems(userId);
                return Ok(superRentCartItems);

            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }





        [HttpPost("CheckoutCart")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> AddItemToCart(CheckoutCartDTO dto)
        {
            try
            {
                int saleId = await _userServices.CheckoutCart(dto.UserId);
                return Ok(saleId);
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
                Cart cartItem = await _userServices.AddItemToCart(addToCartDTO.UserId, addToCartDTO.BookId, addToCartDTO.Quantity);
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
                Cart cartItem = await _userServices.RemoveItemFromCart(deleteItemFromCartDTO.UserId, deleteItemFromCartDTO.BookId);
                return Ok(cartItem);
                
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }

        [HttpPost("GiveFeedBack")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> AddItemToCart(GiveFeedback dto)
        {
            try
            {
                Feedback feedback = await _userServices.GiveFeedback(dto);
                return Ok(feedback.FeedbackId);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


        [HttpPost("ViewFeedback")]
        [ProducesResponseType(typeof(ViewFeedbackDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> ViewFeedback(int BookId)
        {
            try
            {
                ViewFeedbackDTO dto = await _userServices.GetFeedbackItems(BookId);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


        [HttpGet("ViewOrders")]
        [ProducesResponseType(typeof(Sale), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnCartDTO>> ViewOrders(int userId)
        {
            try
            {
               var orders = await _userServices.ViewOrders(userId);
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
                var saleDetails = await _userServices.ViewOrderDetail(saleId);
                return Ok(saleDetails);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }


        }


    }
}
