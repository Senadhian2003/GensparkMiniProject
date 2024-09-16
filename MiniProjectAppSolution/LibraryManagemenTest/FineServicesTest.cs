using MiniProjectApp.Exceptions;
using MiniProjectApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagemenTest
{
    public class FineServicesTest : BaseSetup
    {

        [Test]
        public async Task ViewFines()
        {

            await _userValidationService.VerifyDue(2);

            var result = await _fineServices.ViewFines(2);


            Assert.That(result.Count, Is.EqualTo(1));
            Assert.Pass();


        }


        [Test]
        public async Task ViewFinesFail()
        {


            var exception = Assert.ThrowsAsync<EmptyListException>(async () => await _fineServices.ViewFines(2));
            Assert.That(exception.Message, Is.EqualTo("The Fine List is empty"));


        }


        [Test]
        public async Task VerifyUnPaidFine()
        {

            await _userValidationService.VerifyDue(2);

            var result = await _fineServices.ViewUnPaidFines(2);


            Assert.That(result.Count, Is.EqualTo(1));
            Assert.Pass();


        }



        [Test]
        public async Task VerifyUnPaidFineFail()
        {

            var exception = Assert.ThrowsAsync<EmptyListException>(async () => await _fineServices.ViewUnPaidFines(2));
            Assert.That(exception.Message, Is.EqualTo("The Fine List is empty"));

        }


        [Test]
        public async Task PayFineFOrOneBookBookNotReturnedFail()
        {
            await _userValidationService.VerifyDue(2);


            var exception = Assert.ThrowsAsync<BooksNotReturnedException>(async () => await _fineServices.PayFineForOneBook(1,2));
            Assert.That(exception.Message, Is.EqualTo("The user has not returned the book with id 2. Return the book before paying the fine"));


        }

        [Test]
        public async Task PayFineForOneBook()
        {
            await _userValidationService.VerifyDue(2);
            ReturnRentedBooksDTO dto = new ReturnRentedBooksDTO();
            dto.UserId = 2;
            List<int> bookIds = new List<int>() { 2 };
            dto.BookIds = bookIds;
            await _rentServices.ReturnRentedBooks(dto);

            var result = await _fineServices.PayFineForOneBook(1, 2);
            Assert.That(result.Status, Is.EqualTo("Fine paid"));
            Assert.Pass();


        }


        [Test]
        public async Task PayFineForAllBooksAlreadyPaidException()
        {
            await _userValidationService.VerifyDue(2);
            ReturnRentedBooksDTO dto = new ReturnRentedBooksDTO();
            dto.UserId = 2;
            List<int> bookIds = new List<int>() { 2 };
            dto.BookIds = bookIds;
            await _rentServices.ReturnRentedBooks(dto);

            await _fineServices.PayFineForOneBook(1, 2);


            var exception = Assert.ThrowsAsync<FineAlreadyPaidException>(async () => await _fineServices.PayFine(1,2));
            Assert.That(exception.Message, Is.EqualTo("The fine has already been paid by the user"));



        }

        [Test]
        public async Task PayFineForAllBooks()
        {
            await _userValidationService.VerifyDue(2);
            ReturnRentedBooksDTO dto = new ReturnRentedBooksDTO();
            dto.UserId = 2;
            List<int> bookIds = new List<int>() { 2 };
            dto.BookIds = bookIds;
            await _rentServices.ReturnRentedBooks(dto);

            var result = await _fineServices.PayFine(1,2);

            Assert.That(result.Status, Is.EqualTo("Fine paid"));
            Assert.Pass();
        }

        [Test]
        public async Task PayFineForAllBooksBooksNotReturned()
        {
            await _userValidationService.VerifyDue(2);


            var exception = Assert.ThrowsAsync<BooksNotReturnedException>(async () => await _fineServices.PayFine(1, 2));
            Assert.That(exception.Message, Is.EqualTo("The user has not returned the book with id 2. Return the book before paying the fine"));

        }







    }
}
