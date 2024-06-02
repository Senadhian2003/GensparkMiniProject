using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Repositories;
using MiniProjectApp.Repositories.Interface;
using MiniProjectApp.Services.Interfaces;
using System.Net;

namespace MiniProjectApp.Services
{
    public class FineServices : IFineServices
    {

        private readonly IRepository<int, Rent> _rentRepository;
        private readonly IRepository<int, Fine> _fineRepository;
        private readonly IRepository<int, RentStock> _rentStockRepository;
        private readonly ICompositeKeyRepository<int, RentCart> _rentCartRepository;
        private readonly ICompositeKeyRepository<int, SuperRentCart> _superRentCartRepository;
        private readonly ICompositeKeyRepository<int, RentDetail> _rentDetailRepository;
        private readonly IRepository<int, UserCredential> _userCredentialRepository;
        private readonly ICompositeKeyRepository<int, FineDetail> _fineDetailRepository;
        public FineServices(IRepository<int, UserCredential> userCredentialRepository, ICompositeKeyRepository<int,FineDetail> fineDetailRepository , IRepository<int, Rent> rentRepository, IRepository<int, Fine> fineRepository, IRepository<int, RentStock> rentStockRepository, ICompositeKeyRepository<int, RentCart> rentCartRepository, ICompositeKeyRepository<int, SuperRentCart> superRentCartRepository, ICompositeKeyRepository<int, RentDetail> rentDetailRepository) 
        {
            _rentRepository = rentRepository;
            _fineRepository = fineRepository;
            _rentStockRepository = rentStockRepository;
            _rentCartRepository = rentCartRepository;
            _superRentCartRepository = superRentCartRepository;
            _rentDetailRepository = rentDetailRepository;
            _userCredentialRepository = userCredentialRepository;
            _fineDetailRepository = fineDetailRepository;
        }

        public async Task<List<Fine>> ViewFines(int UserId)
        {

            var Fines = await _fineRepository.GetAll();

            var userFines = Fines.Where(f => f.UserId == UserId);

            if (userFines.Any())
            {
                return userFines.ToList();
            }

            throw new EmptyListException("Fine");
        }

        public async Task<List<Fine>> ViewUnPaidFines(int UserId)
        {

            var Fines = await _fineRepository.GetAll();

            var userFines = Fines.Where(f => f.UserId == UserId && f.Status == "Fine to be paid");

            if (userFines.Any())
            {
                return userFines.ToList();
            }

            throw new EmptyListException("Fine");
        }

        public async Task<FineDetail> PayFineForOneBook(int RentId, int BookId)
        {

            RentDetail rentDetail = await _rentDetailRepository.GetByKey(RentId, BookId);

            if (rentDetail.status != "Returned")
            {
                throw new BooksNotReturnedException(BookId);
            }


            FineDetail fineDetail = await _fineDetailRepository.GetByKey(RentId, BookId);

            fineDetail.FinePaidDate = DateTime.Now;
            fineDetail.Status = "Fine Paid";

            Fine fine = await _fineRepository.GetByKey(RentId);

            fine.FinePending -= fineDetail.FineAmount;

            if (fine.NumbeOfBooksPaidFine == 0)
            {
                fine.Status = "Fine paid";
            }

            await _fineRepository.Update(fine);

            return fineDetail;

        }

        public async Task<Fine> PayFine(int RentId, int UserId)
        {

            Fine fine = await _fineRepository.GetByKey(RentId);
            if (fine.Status == "Fine paid")
            {
                throw new FineAlreadyPaidException();
            }

            var fineDetails = await _fineDetailRepository.GetAll();

            var userFineDetails = fineDetails.Where(fd => fd.RentId == RentId);

            int cnt = 0;
            double amountPaid = 0;
            foreach (var fineDetail in userFineDetails)
            {

                RentDetail rentDetail = await _rentDetailRepository.GetByKey(fineDetail.RentId, fineDetail.BookId);

                if (rentDetail.status != "Returned")
                {
                    throw new BooksNotReturnedException(fineDetail.BookId);
                }


                fineDetail.FinePaidDate = DateTime.Now;
                fineDetail.Status = "Fine Paid";
                await _fineDetailRepository.Update(fineDetail);
                cnt++;
                amountPaid+=5;

            }

            fine.NumbeOfBooksPaidFine += cnt;
            fine.FinePending -= amountPaid;
            fine.Status = "Fine paid";

            await _fineRepository.Update(fine);

            
     

           


            //var rent = await _rentRepository.GetByKey(RentId);

            //rent.Progress = "Fine paid";
            //rent.BooksToBeReturned = 0;

            //await _rentRepository.Update(rent);

            //var rentDetails = rent.RentDetailsList.ToList();

            //foreach (RentDetail rentDetail in rentDetails)
            //{
            //    if (rentDetail.status == "Fine to be paid")
            //    {
            //        rentDetail.status = "Returned";

            //        RentStock bookStock = await _rentStockRepository.GetByKey(rentDetail.BookId);
            //        bookStock.QuantityInStock += 1;
            //        await _rentStockRepository.Update(bookStock);

            //        await _rentDetailRepository.Update(rentDetail);


            //        if (rent.CartType == "Super Cart")
            //        {
            //            await _superRentCartRepository.DeleteByKey(UserId, rentDetail.BookId);

            //        }
            //        else
            //        {
            //            await _rentCartRepository.DeleteByKey(UserId, rentDetail.BookId);
            //        }

            //    }
            //}
            
            return fine;

        }


      


    }
}
