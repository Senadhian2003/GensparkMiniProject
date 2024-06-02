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
        private readonly IRepository<int, User> _userRepository;


        public SaleServices(IRepository<int, User> userRepository,IRepository<int, Sale> saleRepository)
        {
            _saleRepository = saleRepository;
            _userRepository = userRepository;
        }


        public async Task<List<Sale>> ViewOrders(int UserId)
        {
            User user = await _userRepository.GetByKey(UserId);
            var sales = await _saleRepository.GetAll();

            var userSales = sales.Where(sales => sales.UserId == UserId);

            if (userSales.Count() == 0)
            {
                throw new EmptyListException("Sales");
            }

            return userSales.ToList();

        }

        public async Task<List<SaleDetail>> ViewOrderDetail(int saleId)
        {

            var sale = await _saleRepository.GetByKey(saleId);

            var saleDetails = sale.SaleDetailList;

            return saleDetails.ToList();

        }

    }
}
