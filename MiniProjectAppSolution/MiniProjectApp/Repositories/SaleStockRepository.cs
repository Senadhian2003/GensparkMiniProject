using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class SaleStockRepository : IRepository<int, SalesStock>
    {

        private readonly LibraryManagementContext _context;
        public SaleStockRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<SalesStock> Add(SalesStock item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<SalesStock> DeleteByKey(int key)
        {
            var saleItem = await GetByKey(key);
            if (saleItem != null)
            {
                _context.Remove(saleItem);
                await _context.SaveChangesAsync(true);
                return saleItem;
            }
            throw new ElementNotFoundException("Sale Item");
        }

        public async Task<SalesStock> GetByKey(int key)
        {
            var saleItem = await _context.SalesStocks.Include(cs => cs.Book)
             .ThenInclude(b => b.Author)
         .Include(cs => cs.Book)
             .ThenInclude(b => b.Publisher).FirstOrDefaultAsync(cs => cs.BookId == key);

            if (saleItem != null)
            {
                return saleItem;
            }

           return null;
        }

        public async Task<IEnumerable<SalesStock>> GetAll()
        {
            var saleItems = await _context.SalesStocks
         .Include(cs => cs.Book)
             .ThenInclude(b => b.Author)
         .Include(cs => cs.Book)
             .ThenInclude(b => b.Publisher)
         .ToListAsync();

            if (saleItems.Any())
            {
                return saleItems;
            }

            throw new EmptyListException("Sale Item");

        }

        public async Task<SalesStock> Update(SalesStock item)
        {
            var saleItem = await GetByKey(item.BookId);
            if (saleItem != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return saleItem;
            }
            throw new ElementNotFoundException("Sale Item");
        }






    }
}
