using MiniProjectApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagemenTest
{
    public class UserValidationServicesTest : BaseSetup
    {


        [Test]
        public async Task VerifyDueSuperRentCart()
        {

            var result = await _userValidationService.VerifyDue(3);

            Assert.That(result.SuperRentCartBooksFined, Is.EqualTo(1));
            Assert.Pass();
            

        }

        [Test]
        public async Task VerifyDueRentCart()
        {

            var result = await _userValidationService.VerifyDue(2);

            Assert.That(result.RentCartBooksFined, Is.EqualTo(1));
            Assert.Pass();


        }

        [Test]
        public async Task VerifyUserPaidFineDisabled()
        {

            await _userValidationService.VerifyDue(2);

            var result = await _userValidationService.VerifyUserPaidFine(2);


            Assert.That(result.Status, Is.EqualTo("Disabled"));
            Assert.Pass();


        }


        [Test]
        public async Task VerifyUserPaidFineActive()
        {
            ReturnRentedBooksDTO dto = new ReturnRentedBooksDTO();
            dto.UserId = 2;
            List<int> bookIds = new List<int>() { 2 };
            dto.BookIds = bookIds;
            await _rentServices.ReturnRentedBooks(dto);
            await _userValidationService.VerifyDue(2);

            var result = await _userValidationService.VerifyUserPaidFine(2);


            Assert.That(result.Status, Is.EqualTo("Active"));
            Assert.Pass();


        }




    }
}
