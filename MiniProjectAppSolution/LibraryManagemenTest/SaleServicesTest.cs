using MiniProjectApp.Exceptions;
using MiniProjectApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagemenTest
{
    public class SaleServicesTest : BaseSetup
    {
        [Test]
        public async Task ViewOrders()
        {
            await _cartServices.AddItemToCart(2, 1, 5);
            await _cartServices.AddItemToCart(2, 2, 5);
            await _cartServices.CheckoutCart(2);

            var result = await _saleServices.ViewOrders(2);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.Pass();

        }

        [Test]
        public async Task ViewOrdersFail()
        {
            await _cartServices.AddItemToCart(2, 1, 5);
            await _cartServices.AddItemToCart(2, 2, 5);
            await _cartServices.CheckoutCart(2);

            var exception = Assert.ThrowsAsync<EmptyListException>(async () => await _saleServices.ViewOrders(3));
            Assert.That(exception.Message, Is.EqualTo("The Sale List is empty"));

        }

        [Test]
        public async Task ViewOrderDetail()
        {

            await _cartServices.AddItemToCart(2, 1, 5);
            await _cartServices.AddItemToCart(2, 2, 5);
            await _cartServices.CheckoutCart(2);
            var result = await _saleServices.ViewOrderDetail(1);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.Pass();

        }

        [Test]
        public async Task ViewOrderDetailFail()
        {

          
            var exception = Assert.ThrowsAsync<ElementNotFoundException>(async () => await _saleServices.ViewOrderDetail(2));
            Assert.That(exception.Message, Is.EqualTo("The Sale does not exist."));

        }

        [Test]
        public async Task ViewRents()
        {
            RentBooksDTO rentBooksDTO = new RentBooksDTO();

            rentBooksDTO.UserId = 2;
            List<int> bookIds = new List<int>() { 5, 6 };
            rentBooksDTO.BookIds = bookIds;
            rentBooksDTO.CartType = "Normal Cart";

            await _rentServices.AddBooksToRent(rentBooksDTO);

            var result = await _saleServices.ViewRents(2);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.Pass();

        }

        [Test]
        public async Task ViewRentsFail()
        {
            

            var exception = Assert.ThrowsAsync<EmptyListException>(async () => await _saleServices.ViewRents(1));
            Assert.That(exception.Message, Is.EqualTo("The Rent List is empty"));

        }


        [Test]
        public async Task ViewRentDetail()
        {
            RentBooksDTO rentBooksDTO = new RentBooksDTO();

            rentBooksDTO.UserId = 2;
            List<int> bookIds = new List<int>() { 5, 6 };
            rentBooksDTO.BookIds = bookIds;
            rentBooksDTO.CartType = "Normal Cart";

            await _rentServices.AddBooksToRent(rentBooksDTO);

            var result = await _saleServices.ViewRentDetail(3);


            Assert.That(result.Count, Is.EqualTo(2));
            Assert.Pass();

        }

       


    }
}
