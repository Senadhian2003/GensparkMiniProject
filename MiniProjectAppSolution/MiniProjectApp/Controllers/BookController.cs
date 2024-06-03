using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Services.Interfaces;

namespace MiniProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookServices _bookServices;
        private readonly ILogger<UserController> _logger;
        public BookController(IBookServices bookServices,ILogger<UserController> logger) 
        {
            _bookServices = bookServices;    
            _logger = logger;
        
        }

        [Authorize(Roles ="Premium User")]
        [HttpGet("ViewBooksForSale")]
        [ProducesResponseType(typeof(List<SalesStock>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SalesStock>>> ViewBooksForSale()
        {
            try
            {
                var salesItems = await _bookServices.GetCurrentSaleBooks();
                return Ok(salesItems);

            }
            catch (EmptyListException ele)
            {
                return NotFound(new ErrorModel(404, ele.Message));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message)); ;
            }


        }


        [HttpPost("GiveFeedBack")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GiveFeedbackForBook(GiveFeedback dto)
        {
            try
            {
                Feedback feedback = await _bookServices.GiveFeedback(dto);
                return Ok(feedback.FeedbackId);
            }
            catch (ElementNotFoundException enfe)
            {
                
                return NotFound(new ErrorModel(404, enfe.Message));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message)); ;
            }


        }


        [HttpPost("ViewFeedback")]
        [ProducesResponseType(typeof(ViewFeedbackDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> ViewFeedback(int BookId)
        {
            try
            {
                ViewFeedbackDTO dto = await _bookServices.GetFeedbackItems(BookId);
                return Ok(dto);
            }
            catch (ElementNotFoundException enfe)
            {
                return NotFound(new ErrorModel(404, enfe.Message));
            }
            catch(EmptyListException ele)
            {
                return NotFound(new ErrorModel(204,ele.Message));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message)); ;
            }


        }


    }
}
