using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using System.Net;

namespace LibraryManagemenTest
{
    public class BookServicesTest : BaseSetup
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ViewBooksForSale()
        {
            var salesItems = await _bookServices.GetCurrentSaleBooks();

            Assert.That(salesItems.Count, Is.EqualTo(7));
            Assert.Pass();
        }

        [Test]
        public async Task ViewBooksForRent()
        {
            var salesItems = await _bookServices.GetCurrentRentBooks();

            Assert.That(salesItems.Count, Is.EqualTo(7));
            Assert.Pass();
        }


        [Test]
        public async Task GiveFeedback()
        {
            GiveFeedback dto = new GiveFeedback() {  BookId=1, Message="Best Story", Rating=4.5 };
            int UserId = 2;
            Feedback feedback = await _bookServices.GiveFeedback(dto, UserId );
            Assert.IsNotNull(feedback);

        }

        [Test]
        public async Task GiveFeedbackUserIdFail()
        {
            GiveFeedback dto = new GiveFeedback() { BookId = 1, Message = "Best Story", Rating = 4.5 };
            //Feedback feedback = await _bookServices.GiveFeedback(dto);
            int UserId = 7;
            var exception = Assert.ThrowsAsync<ElementNotFoundException>(async () => await _bookServices.GiveFeedback(dto,UserId));
            Assert.That(exception.Message, Is.EqualTo("The User does not exist."));

        }

        [Test]
        public async Task GiveFeedbackBookIdFail()
        {
            GiveFeedback dto = new GiveFeedback() { BookId = 70, Message = "Best Story", Rating = 4.5 };
            //Feedback feedback = await _bookServices.GiveFeedback(dto);
            int UserId = 2;
            var exception = Assert.ThrowsAsync<ElementNotFoundException>(async () => await _bookServices.GiveFeedback(dto,UserId));
            Assert.That(exception.Message, Is.EqualTo("The Book does not exist."));

        }

        [Test]  
        public async Task GetFeedback()
        {
            GiveFeedback dto = new GiveFeedback() {  BookId = 1, Message = "Best Story", Rating = 4.5 };
            int UserId = 2;
            Feedback feedback = await _bookServices.GiveFeedback(dto, UserId);
          

            GiveFeedback dto2 = new GiveFeedback() { BookId = 1, Message = "Average", Rating = 3.5 };
             
            await _bookServices.GiveFeedback(dto2, UserId);
           


            ViewFeedbackDTO result = await _bookServices.GetFeedbackItems(1);
            var feedbackList = result.feedbacks.ToList();
            Assert.That(feedbackList.Count, Is.EqualTo(2));
            Assert.Pass();

        }

        [Test]
        public async Task GetFeedbackFail()
        {
            GiveFeedback dto = new GiveFeedback() { BookId = 1, Message = "Best Story", Rating = 4.5 };
            int UserId = 2;
            Feedback feedback = await _bookServices.GiveFeedback(dto, UserId);


            GiveFeedback dto2 = new GiveFeedback() { BookId = 1, Message = "Average", Rating = 3.5 };
         
            await _bookServices.GiveFeedback(dto2, UserId);



            //ViewFeedbackDTO result = await _bookServices.GetFeedbackItems(79);

            var exception = Assert.ThrowsAsync<ElementNotFoundException>(async () => await _bookServices.GetFeedbackItems(79));
            Assert.That(exception.Message, Is.EqualTo("The Book does not exist."));

        }

        [Test]
        public async Task GetFeedbackEmptyListFail()
        {
            GiveFeedback dto = new GiveFeedback() { BookId = 1, Message = "Best Story", Rating = 4.5 };
            int UserId = 2;
            Feedback feedback = await _bookServices.GiveFeedback(dto, UserId);


            GiveFeedback dto2 = new GiveFeedback() { BookId = 1, Message = "Average", Rating = 3.5 };
            await _bookServices.GiveFeedback(dto2, UserId);



            //ViewFeedbackDTO result = await _bookServices.GetFeedbackItems(79);

            var exception = Assert.ThrowsAsync<NoFeedbackException>(async () => await _bookServices.GetFeedbackItems(2));
            Assert.That(exception.Message, Is.EqualTo("No feedback for the Book with Id 2"));

        }


    }
}