using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagemenTest
{
    public class CartServicesTest : BaseSetup
    {


        [Test]
        public async Task AddItemsToCart()
        {
            var cart = await _cartServices.AddItemToCart(1,1,5);

           Assert.IsNotNull(cart);
            Assert.Pass();
        }

        [Test]
        public async Task AddItemsToCartUserIdFail()
        {
           

            var exception = Assert.ThrowsAsync<ElementNotFoundException>(async () => await _cartServices.AddItemToCart(7, 1, 5));
            Assert.That(exception.Message, Is.EqualTo("The User does not exist."));
        }

        [Test]
        public async Task AddItemsToCartBookIdFail()
        {


            var exception = Assert.ThrowsAsync<ElementNotFoundException>(async () => await _cartServices.AddItemToCart(1, 50, 5));
            Assert.That(exception.Message, Is.EqualTo("The Book does not exist."));
        }

        [Test]
        public async Task AddItemsToCartOutofStock()
        {

            var exception = Assert.ThrowsAsync<OutOfStockException>(async () => await _cartServices.AddItemToCart(1, 1, 30));
            Assert.That(exception.Message, Is.EqualTo("The required Book with id 1 has only 10 items in stock. Please reduce the quantity to continue purchase"));
        }

        [Test]
        public async Task UpdateCartItemQuantity()
        {
             await _cartServices.AddItemToCart(1, 1, 5);

            var result = await _cartServices.AddItemToCart(1, 1, 3);

            Assert.That(result.Quantity, Is.EqualTo(3));
            Assert.Pass();
        }

        [Test]
        public async Task RemoveItemFromCart()
        {

            await _cartServices.AddItemToCart(1, 1, 3);
            var result = await _cartServices.RemoveItemFromCart(1, 1);

           Assert.NotNull(result);
            Assert.Pass();
        }

        [Test]
        public async Task RemoveItemFromCartFail()
        {

            await _cartServices.AddItemToCart(1, 1, 3);
            

            var exception = Assert.ThrowsAsync<ElementNotFoundException>(async () => await _cartServices.RemoveItemFromCart(1, 2));
            Assert.That(exception.Message, Is.EqualTo("The Cart Item does not exist."));
           
        }



        [Test]
        public async Task CheckoutCart()
        {
            await _cartServices.AddItemToCart(2, 1, 5);
            await _cartServices.AddItemToCart(2, 2, 5);
            var result = await _cartServices.CheckoutCart(2);
            
            Assert.That(result.FinalAmount, Is.EqualTo(400));
            Assert.Pass();

        }

        [Test]
        public async Task CheckoutCartPremiumUser()
        {
            await _cartServices.AddItemToCart(3, 1, 5);
            await _cartServices.AddItemToCart(3, 2, 5);
            var result = await _cartServices.CheckoutCart(3);

            Assert.That(result.FinalAmount, Is.EqualTo(240));
            Assert.Pass();

        }


        [Test]
        public async Task CheckoutCartEmptyCartFail()
        {
           
            

            var exception = Assert.ThrowsAsync<EmptyListException>(async () => await _cartServices.CheckoutCart(2));
            Assert.That(exception.Message, Is.EqualTo("The Cart List is empty"));

        }

        [Test]
        public async Task CheckoutCartOutOfStockFail()
        {
            await _cartServices.AddItemToCart(2, 1, 10);
            await _cartServices.AddItemToCart(3, 1, 5);
            await _cartServices.CheckoutCart(3);


            var exception = Assert.ThrowsAsync<OutOfStockException>(async () => await _cartServices.CheckoutCart(2));
            Assert.That(exception.Message, Is.EqualTo("The required Book with id 1 has only 5 items in stock. Please reduce the quantity to continue purchase"));

        }


        [Test]
        public async Task GetCartItems()
        {
            await _cartServices.AddItemToCart(2, 1, 5);
            await _cartServices.AddItemToCart(2, 2, 5);

            var result = await _cartServices.GetCartItems(2);
            
            Assert.That(result.Items.Count, Is.EqualTo(2));

        }

        [Test]
        public async Task GetCartItemsFail()
        {

            var exception = Assert.ThrowsAsync<EmptyListException>(async () => await _cartServices.GetCartItems(2));
            Assert.That(exception.Message, Is.EqualTo("The Cart List is empty"));

        }

        [Test]
        public async Task GetRentCartItems()
        {
            RentBooksDTO dto = new RentBooksDTO();
            dto.UserId = 3;
            dto.CartType = "Normal Cart";
            List<int> bookIds = new List<int> { 1, 2 };
            dto.BookIds = bookIds;

           var book = await _rentServices.AddBooksToRent(dto);

            var result = await _cartServices.GetRentCartItems(3);
            
            Assert.That(result.Count, Is.EqualTo(2));

        }



        [Test]
        public async Task GetRentCartItemsFail()
        {

            var exception = Assert.ThrowsAsync<EmptyListException>(async () => await _cartServices.GetRentCartItems(3));
            Assert.That(exception.Message, Is.EqualTo("The Rent Cart List is empty"));

        }


        [Test]
        public async Task GetSuperRentCartItems()
        {
            RentBooksDTO dto = new RentBooksDTO();
            dto.UserId = 3;
            dto.CartType = "Super Cart";
            List<int> bookIds = new List<int> { 1, 2 };
            dto.BookIds = bookIds;

            var book = await _rentServices.AddBooksToRent(dto);

            var result = await _cartServices.GetSuperRentCartItems(3);

            Assert.That(result.Count, Is.EqualTo(3));

        }


        [Test]
        public async Task GetSuperRentCartItemsFail()
        {

            var exception = Assert.ThrowsAsync<EmptyListException>(async () => await _cartServices.GetSuperRentCartItems(2));
            Assert.That(exception.Message, Is.EqualTo("The Super Rent Cart List is empty"));

        }




    }
}
