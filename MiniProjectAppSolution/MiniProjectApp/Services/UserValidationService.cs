using Microsoft.AspNetCore.Identity;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Repositories;
using MiniProjectApp.Repositories.Interface;
using MiniProjectApp.Services.Interfaces;

namespace MiniProjectApp.Services
{
    public class UserValidationService : IUserValidationService
    {
        private readonly IRepository<int, Rent> _rentRepository;
        private readonly IRepository<int, UserCredential> _userCredentialRepository;
        private readonly IRepository<int, Fine> _fineRepository;
        private readonly ICompositeKeyRepository<int, RentDetail> _rentDetailRepository;
        private readonly IRepository<int, User> _userRepository;
        private readonly ICompositeKeyRepository<int, FineDetail> _fineDetailRepository;
        private readonly ICompositeKeyRepository<int, SuperRentCart> _superRentCartRepository;
        private readonly ICompositeKeyRepository<int, RentCart> _rentCartRepository;
        public UserValidationService(IRepository<int, Rent> rentRepository, IRepository<int,User> userRepository , ICompositeKeyRepository<int,FineDetail> fineDetailRepository, IRepository<int, UserCredential> userCredentialRepository, IRepository<int, Fine> fineRepository, ICompositeKeyRepository<int, RentDetail> rentDetailRepository, ICompositeKeyRepository<int,RentCart> rentCartRepository, ICompositeKeyRepository<int,SuperRentCart> superRentCartRepository) 
        {
            _rentRepository = rentRepository;
            _userCredentialRepository = userCredentialRepository;
            _fineRepository = fineRepository;
            _rentDetailRepository = rentDetailRepository;
            _userRepository = userRepository;
            _fineDetailRepository = fineDetailRepository;
            _rentCartRepository = rentCartRepository;
            _superRentCartRepository = superRentCartRepository;

        }




        public async Task<UserStatusDTO> VerifyUserPaidFine(int UserId)
        {
            UserCredential userCredential = await _userCredentialRepository.GetByKey(UserId);

            //var rents = await _rentRepository.GetAll();
            //var fineRents = rents.Where(r => r.UserId == UserId && r.Progress == "Fine to be paid");

            var fines = await _fineRepository.GetAll();
            var userFines = fines.Where(f => f.UserId == UserId && f.Status == "Fine to be paid");



            if (userFines.Any())
            {
                userCredential.Status = "Disabled";
                await _userCredentialRepository.Update(userCredential);
                UserStatusDTO userStatusDTO = new UserStatusDTO();
                userStatusDTO.UserId = UserId;
                userStatusDTO.Status = "Disabled";
                return userStatusDTO;

            }
            else
            {
                userCredential.Status = "Active";
                await _userCredentialRepository.Update(userCredential);
                UserStatusDTO userStatusDTO = new UserStatusDTO();
                userStatusDTO.UserId = UserId;
                userStatusDTO.Status = "Active";
                return userStatusDTO;
            }

        }


        public async Task VerifyDue(int userId)
        {

            //var rents = await _rentRepository.GetAll();

            //var userRents = rents.Where(r => r.UserId == userId && r.Progress == "Return pending" && DateTime.Now > r.DueDate);


            //bool flag = false;




            //if (userRents.Any())
            //{
            //    flag = true;
            //    foreach (var rent in userRents)
            //    {
            //        bool fineCreate = false;
            //        rent.Progress = "Fine to be paid";
            //        rent.FineAmount = rent.BooksToBeReturned * 5;
            //        await _rentRepository.Update(rent);

            //        Fine fine = new Fine();

            //        fine.RentId = rent.RentId;
            //        fine.UserId = rent.UserId;
            //        fine.FineAmount = rent.FineAmount;
            //        fine.NumberOfBooksFined = rent.BooksToBeReturned;
            //        fine.Status = "Fine to be paid";

            //        await _fineRepository.Add(fine);

            //        var rentDetails = rent.RentDetailsList;

            //        foreach (var rentDetail in rentDetails)
            //        {

            //            if (rentDetail.status != "Returned")
            //            {

            //                rentDetail.status = "Fine to be paid";
            //                await _rentDetailRepository.Update(rentDetail);
            //            }

            //        }



            //    }

            //}

            //return flag;


            User user = await _userRepository.GetByKey(userId);

            var rentCart = user.RentCartItems.Where(rc=>DateTime.Now>rc.DueDate && rc.IsFined==0).GroupBy(rc => rc.RentId).ToList();
            var superCart = user.SuperRentCartItems.Where(rc => DateTime.Now > rc.DueDate && rc.IsFined == 0).GroupBy(rc => rc.RentId).ToList();

            foreach (var group in rentCart)
            {
                int rentId = group.Key;
                Console.WriteLine($"RentId: {rentId}");
                Fine fine = new Fine();

                Rent rent = await _rentRepository.GetByKey(rentId);   
                fine.RentId = rent.RentId;
                fine.UserId = rent.UserId;
                
                fine.Status = "Fine to be paid";
                int cnt = 0;
                foreach (var item in group)
                {
                    // Access each RentCart item in the group
                    Console.WriteLine($"UserId: {item.UserId}, BookId: {item.BookId}, DueDate: {item.DueDate}, IsFined: {item.IsFined}");

                    FineDetail fineDetail = new FineDetail();

                    fineDetail.RentId = item.RentId;
                    fineDetail.BookId = item.BookId;
                    fineDetail.Status = "Fine to be paid";
                    fineDetail.FineAmount = CalculateFineForOneBook(); 
                    cnt++;
                    await _fineDetailRepository.Add(fineDetail);

                    RentCart rentCartItem = await _rentCartRepository.GetByKey(userId, item.BookId);
                    rentCartItem.IsFined = 1;
                    await _rentCartRepository.Update(rentCartItem);

                    // You can perform other operations with the item here
                }

                fine.NumberOfBooksFined = cnt;
                fine.FineAmount = cnt * CalculateFineForOneBook();
                fine.FinePending = cnt * CalculateFineForOneBook();
                await _fineRepository.Add(fine);


            }


            foreach (var group in superCart)
            {
                int rentId = group.Key;
                Console.WriteLine($"RentId: {rentId}");
                Fine fine = new Fine();

                Rent rent = await _rentRepository.GetByKey(rentId);
                fine.RentId = rent.RentId;
                fine.UserId = rent.UserId;

                fine.Status = "Fine to be paid";
                int cnt = 0;
                foreach (var item in group)
                {
                    // Access each RentCart item in the group
                    Console.WriteLine($"UserId: {item.UserId}, BookId: {item.BookId}, DueDate: {item.DueDate}, IsFined: {item.IsFined}");

                    FineDetail fineDetail = new FineDetail();

                    fineDetail.RentId = item.RentId;
                    fineDetail.BookId = item.BookId;
                    fineDetail.FineAmount = CalculateFineForOneBook();
                    fineDetail.Status = "Fine to be paid";
                    cnt++;
                    await _fineDetailRepository.Add(fineDetail);

                    SuperRentCart superRentItem = await _superRentCartRepository.GetByKey(userId, item.BookId);

                    superRentItem.IsFined = 1;
                    await _superRentCartRepository.Update(superRentItem);

                    // You can perform other operations with the item here
                }

                fine.NumberOfBooksFined = cnt;
                fine.FineAmount = cnt * CalculateFineForOneBook();
                fine.FinePending = cnt * CalculateFineForOneBook();
                await _fineRepository.Add(fine);


            }





        }

        public float CalculateFineForOneBook()
        {
            return 5;
        }

    }
}
