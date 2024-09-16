using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace MiniProjectApp.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class BookController : ControllerBase
    {

        private readonly IBookServices _bookServices;
        private readonly ILogger<BookController> _logger;
        public BookController(IBookServices bookServices,ILogger<BookController> logger) 
        {
            _bookServices = bookServices;    
            _logger = logger;
        
        }

        //[Authorize(Roles = "User,Premium User,Admin")]
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


        //[Authorize(Roles = "User,Premium User,Admin")]
        [HttpGet("ViewSaleBookDetail")]
        [ProducesResponseType(typeof(List<SalesStock>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SalesStock>> ViewSaleBookDetail(int bookId)
        {
            try
            {
                var salesItem = await _bookServices.ViewSaleBookDetail(bookId);
                return Ok(salesItem);

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

        //[Authorize(Roles = "User,Premium User,Admin")]
        [HttpGet("ViewBooksForRent")]
        [ProducesResponseType(typeof(List<RentStock>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<RentStock>>> ViewBooksForRent()
        {
            try
            {
                var RentItems = await _bookServices.GetCurrentRentBooks();
                return Ok(RentItems);

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

        [HttpGet("GetAllAuthors")]
        [ProducesResponseType(typeof(List<Author>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SalesStock>>> GetAllAuthors()
        {
            try
            {
                var authors = await _bookServices.GetAllAuthors();
                return Ok(authors);

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

        [HttpGet("GetUniqueCategories")]
        [ProducesResponseType(typeof(List<Author>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SalesStock>>> GetUniqueCategories()
        {
            try
            {
                var categories = await _bookServices.GetUniqueCategories();
                return Ok(categories);

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


        [Authorize(Roles = "User,Premium User")]
        [HttpPost("GiveFeedBack")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GiveFeedbackForBook(GiveFeedback dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new ErrorModel(400, string.Join("; ", errors)));
                }


                var userstring = User.Claims?.FirstOrDefault(x => x.Type == "Id")?.Value;
                var userid = Convert.ToInt32(userstring);
                Feedback feedback = await _bookServices.GiveFeedback(dto,userid);
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

        
        //[Authorize(Roles = "User,Premium User,Admin")]
        [HttpGet("ViewFeedback")]
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



        [HttpGet("ViewAllBookDetails")]
        [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SalesStock>>> ViewAllBookDetails()
        {
            try
            {
                var books = await _bookServices.GetAllBookDetails();
                return Ok(books);

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

        [HttpGet("GetAllPUblishers")]
        [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SalesStock>>> GetAllPublishers()
        {
            try
            {
                var publishers = await _bookServices.GetALlPublishers();
                return Ok(publishers);

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


        [HttpPost("AddNewAuthor")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AddNewAuthor(AddNewAuthorDTO authorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new ErrorModel(400, string.Join("; ", errors)));
                }

                Author author = await _bookServices.AddNewAuthor(authorDto);
                return Ok(author);
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


        [HttpPost("AddNewPublisher")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AddNewPublisher(AddNewPublisherDTO publisherDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new ErrorModel(400, string.Join("; ", errors)));
                }

                Publisher publisher = await _bookServices.AddNewPublisher(publisherDto);
                return Ok(publisher);
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

        [HttpPost("AddNewBook")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AddNewBook([FromForm] AddNewBookDTO bookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new ErrorModel(400, string.Join("; ", errors)));
                }

               Book book = await _bookServices.AddNewBook(bookDto);
                return Ok(book);
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


    }
}
