using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories;
using MiniProjectApp.Repositories.Interface;
using MiniProjectApp.Services.Interfaces;

namespace MiniProjectApp.Services
{
    public class SaleServices : ISaleServices
    {
        private readonly IRepository<int, Sale> _saleRepository;
        private readonly IRepository<int, Rent> _rentRepository;
        private readonly IRepository<int, User> _userRepository;


        public SaleServices(IRepository<int, User> userRepository,IRepository<int, Sale> saleRepository, IRepository<int, Rent> rentRepository)
        {
            _saleRepository = saleRepository;
            _userRepository = userRepository;
            _rentRepository = rentRepository;
        }


        public async Task<List<Sale>> ViewOrders(int UserId)
        {
            User user = await _userRepository.GetByKey(UserId);
            var sales = await _saleRepository.GetAll();

            var userSales = sales.Where(sales => sales.UserId == UserId);

            if (userSales.Count() == 0)
            {
                throw new EmptyListException("Sale");
            }

            return userSales.ToList();

        }

        public async Task<List<SaleDetail>> ViewOrderDetail(int saleId)
        {

            var sale = await _saleRepository.GetByKey(saleId);

            var saleDetails = sale.SaleDetailList;

            return saleDetails.ToList();

        }

        public async Task<List<Rent>> ViewRents(int UserId)
        {
            User user = await _userRepository.GetByKey(UserId);
            var rents = await _rentRepository.GetAll();

            var userRents = rents.Where(rent => rent.UserId == UserId);

            if (userRents.Count() == 0)
            {
                throw new EmptyListException("Rent");
            }

            return userRents.ToList();

        }

        public async Task<List<Rent>> ViewAllRents()
        {
            
            var rents = await _rentRepository.GetAll();

           

            if (rents.Count() == 0)
            {
                throw new EmptyListException("Rent");
            }

            return rents.ToList();

        }


        public async Task<List<RentDetail>> ViewRentDetail(int rentId)
        {

            var rent = await _rentRepository.GetByKey(rentId);

            var rentDetails = rent.RentDetailsList;

            return rentDetails.ToList();

        }


    }
}
