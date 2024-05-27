using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class SaleRepository : IRepository<int, Sale>
    {

        private readonly LibraryManagementContext _context;
        public SaleRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<Sale> Add(Sale item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Sale> DeleteByKey(int key)
        {
            var sales = await GetByKey(key);
            if (sales != null)
            {
                _context.Remove(sales);
                await _context.SaveChangesAsync(true);
                return sales;
            }
            throw new ElementNotFoundException("Sale");
        }

        public async Task<Sale> GetByKey(int key)
        {
            var sales = await _context.Sales.FirstOrDefaultAsync(u => u.UserId == key);

            if (sales != null)
            {
                return sales;
            }

            throw new ElementNotFoundException("Sale");
        }

        public async Task<IEnumerable<Sale>> GetAll()
        {
            var users = await _context.Sales.ToListAsync();

            if (users.Any())
            {
                return users;
            }

            throw new EmptyListException("Sale");

        }

        public async Task<Sale> Update(Sale item)
        {
            var sales = await GetByKey(item.UserId);
            if (sales != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return sales;
            }
            throw new ElementNotFoundException("Sale");
        }



    }
}
