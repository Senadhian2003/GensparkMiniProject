using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class RentStockRepository : IRepository<int,RentStock>
    {
        private readonly LibraryManagementContext _context;
        public RentStockRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<RentStock> Add(RentStock item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<RentStock> DeleteByKey(int key)
        {
            var rentItem = await GetByKey(key);
            if (rentItem != null)
            {
                _context.Remove(rentItem);
                await _context.SaveChangesAsync(true);
                return rentItem;
            }
            throw new ElementNotFoundException("Rent Item");
        }

        public async Task<RentStock> GetByKey(int key)
        {
            var rentItem = await _context.RentStocks.FirstOrDefaultAsync(rs => rs.BookId == key);

            if (rentItem != null)
            {
                return rentItem;
            }

            return null;
        }

        public async Task<IEnumerable<RentStock>> GetAll()
        {
            var rentItems = await _context.RentStocks.Include(rs => rs.Book).ToListAsync();

            if (rentItems.Any())
            {
                return rentItems;
            }

            throw new EmptyListException("Rent Item");

        }

        public async Task<RentStock> Update(RentStock item)
        {
            var rentItem = await GetByKey(item.BookId);
            if (rentItem != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return rentItem;
            }
            throw new ElementNotFoundException("Rent Item");
        }




    }
}
