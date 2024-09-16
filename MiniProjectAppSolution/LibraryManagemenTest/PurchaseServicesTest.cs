using MiniProjectApp.Exceptions;
using MiniProjectApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagemenTest
{
    public class PurchaseServicesTest : BaseSetup
    {
        [Test]
        public async Task PurchasePresentBooksForSale()
        {

            PurchaseBooksForLibraryDTO dto = new PurchaseBooksForLibraryDTO();

            PurchaseItemDTO item1 = new PurchaseItemDTO() { BookId= 1, PricePerBook=10, Quantity=5  };
            PurchaseItemDTO item2 = new PurchaseItemDTO() { BookId= 2, PricePerBook=5, Quantity = 5 };

            List <PurchaseItemDTO> list = new List<PurchaseItemDTO>();
            list.Add(item1);
            list.Add(item2);

            dto.Type = "Sale";

            dto.Items = list;

            var result =  await _purchaseServices.PurchaseBooksForLibrary(dto);

            Assert.IsNotNull(result);
            Assert.Pass();
           

        }

        [Test]
        public async Task PurchaseBooksForSaleEmptyInputFail()
        {

            PurchaseBooksForLibraryDTO dto = new PurchaseBooksForLibraryDTO();

           
            List<PurchaseItemDTO> list = new List<PurchaseItemDTO>();
           

            dto.Type = "Sale";

            dto.Items = list;

           

            var exception = Assert.ThrowsAsync<EmptyListException>(async () => await _purchaseServices.PurchaseBooksForLibrary(dto));
            Assert.That(exception.Message, Is.EqualTo("The Input Books List is empty"));


        }

        [Test]
        public async Task PurchaseNewBooksForSale()
        {

            PurchaseBooksForLibraryDTO dto = new PurchaseBooksForLibraryDTO();

            PurchaseItemDTO item1 = new PurchaseItemDTO() { BookId = 8, PricePerBook = 10, Quantity = 5 };
            PurchaseItemDTO item2 = new PurchaseItemDTO() { BookId = 9, PricePerBook = 5, Quantity = 5 };

            List<PurchaseItemDTO> list = new List<PurchaseItemDTO>();
            list.Add(item1);
            list.Add(item2);

            dto.Type = "Sale";

            dto.Items = list;

            var result = await _purchaseServices.PurchaseBooksForLibrary(dto);

            Assert.IsNotNull(result);
            Assert.Pass();


        }


        [Test]
        public async Task PurchasePresentBooksForRent()
        {

            PurchaseBooksForLibraryDTO dto = new PurchaseBooksForLibraryDTO();

            PurchaseItemDTO item1 = new PurchaseItemDTO() { BookId = 1, PricePerBook = 10, Quantity = 5 };
            PurchaseItemDTO item2 = new PurchaseItemDTO() { BookId = 2, PricePerBook = 5, Quantity = 5 };

            List<PurchaseItemDTO> list = new List<PurchaseItemDTO>();
            list.Add(item1);
            list.Add(item2);

            dto.Type = "Rent";

            dto.Items = list;

            var result = await _purchaseServices.PurchaseBooksForLibrary(dto);

            Assert.IsNotNull(result);
            Assert.Pass();


        }


        [Test]
        public async Task PurchaseBooksForRentEmptyInputFail()
        {

            PurchaseBooksForLibraryDTO dto = new PurchaseBooksForLibraryDTO();


            List<PurchaseItemDTO> list = new List<PurchaseItemDTO>();


            dto.Type = "Rent";

            dto.Items = list;



            var exception = Assert.ThrowsAsync<EmptyListException>(async () => await _purchaseServices.PurchaseBooksForLibrary(dto));
            Assert.That(exception.Message, Is.EqualTo("The Input Books List is empty"));


        }

        [Test]
        public async Task PurchaseNewBooksForRent()
        {

            PurchaseBooksForLibraryDTO dto = new PurchaseBooksForLibraryDTO();

            PurchaseItemDTO item1 = new PurchaseItemDTO() { BookId = 8, PricePerBook = 10, Quantity = 5 };
            PurchaseItemDTO item2 = new PurchaseItemDTO() { BookId = 9, PricePerBook = 5, Quantity = 5 };

            List<PurchaseItemDTO> list = new List<PurchaseItemDTO>();
            list.Add(item1);
            list.Add(item2);

            dto.Type = "Rent";

            dto.Items = list;

            var result = await _purchaseServices.PurchaseBooksForLibrary(dto);

            Assert.IsNotNull(result);
            Assert.Pass();


        }

        [Test]
        public async Task ViewPurchases()
        {

            PurchaseBooksForLibraryDTO dto = new PurchaseBooksForLibraryDTO();

            PurchaseItemDTO item1 = new PurchaseItemDTO() { BookId = 1, PricePerBook = 10, Quantity = 5 };
            PurchaseItemDTO item2 = new PurchaseItemDTO() { BookId = 2, PricePerBook = 5, Quantity = 5 };

            List<PurchaseItemDTO> list = new List<PurchaseItemDTO>();
            list.Add(item1);
            list.Add(item2);

            dto.Type = "Sale";

            dto.Items = list;

            await _purchaseServices.PurchaseBooksForLibrary(dto);




            var result = await _purchaseServices.ViewPurchase();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.Pass();

        }



        [Test]
        public async Task ViewPurchaseDetails()
        {

            PurchaseBooksForLibraryDTO dto = new PurchaseBooksForLibraryDTO();

            PurchaseItemDTO item1 = new PurchaseItemDTO() { BookId = 1, PricePerBook = 10, Quantity = 5 };
            PurchaseItemDTO item2 = new PurchaseItemDTO() { BookId = 2, PricePerBook = 5, Quantity = 5 };

            List<PurchaseItemDTO> list = new List<PurchaseItemDTO>();
            list.Add(item1);
            list.Add(item2);

            dto.Type = "Sale";

            dto.Items = list;

            await _purchaseServices.PurchaseBooksForLibrary(dto);




            var result = await _purchaseServices.ViewPurchaseDetails(1);

            Assert.IsNotNull(result);
            Assert.Pass();

        }


        [Test]
        public async Task ViewPurchaseDetailsFail()
        {

            PurchaseBooksForLibraryDTO dto = new PurchaseBooksForLibraryDTO();

            PurchaseItemDTO item1 = new PurchaseItemDTO() { BookId = 1, PricePerBook = 10, Quantity = 5 };
            PurchaseItemDTO item2 = new PurchaseItemDTO() { BookId = 2, PricePerBook = 5, Quantity = 5 };

            List<PurchaseItemDTO> list = new List<PurchaseItemDTO>();
            list.Add(item1);
            list.Add(item2);

            dto.Type = "Sale";

            dto.Items = list;

            await _purchaseServices.PurchaseBooksForLibrary(dto);

            var exception = Assert.ThrowsAsync<ElementNotFoundException>(async () => await _purchaseServices.ViewPurchaseDetails(2));
            Assert.That(exception.Message, Is.EqualTo("The Purchase does not exist."));

        }





    }
}
