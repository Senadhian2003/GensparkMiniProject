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
            var sale = await GetByKey(key);
            if (sale != null)
            {
                _context.Remove(sale);
                await _context.SaveChangesAsync(true);
                return sale;
            }
            throw new ElementNotFoundException("Sale");
        }

        public async Task<Sale> GetByKey(int key)
        {
            var sale = await _context.Sales.Include(s=>s.SaleDetailList).ThenInclude(sd=>sd.Book).FirstOrDefaultAsync(s => s.SaleId == key);

            if (sale != null)
            {
                return sale;
            }

            throw new ElementNotFoundException("Sale");
        }

        public async Task<IEnumerable<Sale>> GetAll()
        {
            var sales = await _context.Sales.ToListAsync();

            if (sales.Any())
            {
                return sales;
            }

            throw new EmptyListException("Sale");

        }

        public async Task<Sale> Update(Sale item)
        {
            var sale = await GetByKey(item.UserId);
            if (sale != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return sale;
            }
            throw new ElementNotFoundException("Sale");
        }



    }
}
