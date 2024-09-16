using MiniProjectApp.Exceptions;
using MiniProjectApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagemenTest
{
    public class RentServicesTest : BaseSetup
    {

        [Test]
        public async Task RentBooksNormalCart()
        {

            RentBooksDTO rentBooksDTO = new RentBooksDTO();

            rentBooksDTO.UserId = 2;
            List<int> bookIds = new List<int>() { 5,6 };
            rentBooksDTO.BookIds = bookIds;
            rentBooksDTO.CartType = "Normal Cart";

            var result = await _rentServices.AddBooksToRent(rentBooksDTO);

            Assert.That(result.BooksCount=2, Is.EqualTo(2));
            Assert.Pass();
           
        }

        [Test]
        public async Task RentBooksSuperCart()
        {

            RentBooksDTO rentBooksDTO = new RentBooksDTO();

            rentBooksDTO.UserId = 3;
            List<int> bookIds = new List<int>() { 5, 6 };
            rentBooksDTO.BookIds = bookIds;
            rentBooksDTO.CartType = "Super Cart";

            var result = await _rentServices.AddBooksToRent(rentBooksDTO);

            Assert.That(result.BooksCount = 2, Is.EqualTo(2));
            Assert.Pass();

        }


        [Test]
        public async Task RentBooksEmptyInputFail()
        {

            RentBooksDTO rentBooksDTO = new RentBooksDTO();

            rentBooksDTO.UserId = 3;
            List<int> bookIds = new List<int>();
            rentBooksDTO.BookIds = bookIds;
            rentBooksDTO.CartType = "Super Cart";

          

            var exception = Assert.ThrowsAsync<NoBooksProvidedException>(async () => await _rentServices.AddBooksToRent(rentBooksDTO));
            Assert.That(exception.Message, Is.EqualTo("Provide atleast one book id to perform the following operation"));

        }

        [Test]
        public async Task RentBooksDuplicateBooks()
        {

            RentBooksDTO rentBooksDTO = new RentBooksDTO();

            rentBooksDTO.UserId = 3;
            List<int> bookIds = new List<int>() { 5, 5 };
            rentBooksDTO.BookIds = bookIds;
            rentBooksDTO.CartType = "Super Cart";

            var exception = Assert.ThrowsAsync<DuplicateBooksException>(async () => await _rentServices.AddBooksToRent(rentBooksDTO));
            Assert.That(exception.Message, Is.EqualTo("User cannot pick same book twice in the same Rent."));

        }


        [Test]
        public async Task RentBooksInvalidBookIdFail()
        {

            RentBooksDTO rentBooksDTO = new RentBooksDTO();

            rentBooksDTO.UserId = 3;
            List<int> bookIds = new List<int>() { 12 };
            rentBooksDTO.BookIds = bookIds;
            rentBooksDTO.CartType = "Super Cart";

            var exception = Assert.ThrowsAsync<BookNotAvailabeForThisOperation>(async () => await _rentServices.AddBooksToRent(rentBooksDTO));
            Assert.That(exception.Message, Is.EqualTo("The book with id 12 is not avalable for Rent"));

        }

         [Test]
        public async Task RentBooksSuperCartNotPremiumUserFail()
        {

            RentBooksDTO rentBooksDTO = new RentBooksDTO();

            rentBooksDTO.UserId = 2;
            List<int> bookIds = new List<int>() { 5, 6 };
            rentBooksDTO.BookIds = bookIds;
            rentBooksDTO.CartType = "Super Cart";

           
            var exception = Assert.ThrowsAsync<NotPremiumUserException>(async () => await _rentServices.AddBooksToRent(rentBooksDTO));
            Assert.That(exception.Message, Is.EqualTo("You have to be a Premium user to use this feature. Upgrade plan to premium to use this feature and obtain discount on purchases"));

        }


        [Test]
        public async Task RentBooksSuperCartItemExceededFail()
        {

            RentBooksDTO rentBooksDTO = new RentBooksDTO();

            rentBooksDTO.UserId = 3;
            List<int> bookIds = new List<int>() { 5, 6,7 };
            rentBooksDTO.BookIds = bookIds;
            rentBooksDTO.CartType = "Super Cart";

            var exception = Assert.ThrowsAsync<BooksInSuperCartNotReturnedException>(async () => await _rentServices.AddBooksToRent(rentBooksDTO));
            Assert.That(exception.Message, Is.EqualTo("The super cart already has 1 Books and can contain only 3 items at a time, please return old books to rent new books"));

        }

        [Test]
        public async Task ReturnRentedBooksSuperCart()
        {

            ReturnRentedBooksDTO dto = new ReturnRentedBooksDTO();
            dto.UserId = 3;
            List<int> bookIds = new List<int>() { 4 };
            dto.BookIds = bookIds;
            var result = await _rentServices.ReturnRentedBooks(dto);

            Assert.That(result.NoOfBooksReturned, Is.EqualTo(1));
            Assert.Pass();


        }

        [Test]
        public async Task ReturnRentedBooksNormalCart()
        {

            ReturnRentedBooksDTO dto = new ReturnRentedBooksDTO();
            dto.UserId = 2;
            List<int> bookIds = new List<int>() { 2 };
            dto.BookIds = bookIds;
            var result = await _rentServices.ReturnRentedBooks(dto);

            Assert.That(result.NoOfBooksReturned, Is.EqualTo(1));
            Assert.Pass();


        }

        [Test]
        public async Task ReturnRentedBooksMismatchBooks()
        {

            ReturnRentedBooksDTO dto = new ReturnRentedBooksDTO();
            dto.UserId = 2;
            List<int> bookIds = new List<int>() { 3 };
            dto.BookIds = bookIds;
           

            var exception = Assert.ThrowsAsync<InvalidUserIdOrBookIdException>(async () => await _rentServices.ReturnRentedBooks(dto));
            Assert.That(exception.Message, Is.EqualTo("The User did not rent the book with id 3. Please provide the correct User Id and Book Id"));


        }





    }
}
