using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class RentCartRepository : ICompositeKeyRepository<int,RentCart>
    {

        private readonly LibraryManagementContext _context;
        public RentCartRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<RentCart> Add(RentCart item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<RentCart> DeleteByKey(int key1, int key2)
        {
            var rentCart = await GetByKey(key1, key2);
            if (rentCart != null)
            {
                _context.Remove(rentCart);
                await _context.SaveChangesAsync(true);
                return rentCart;
            }
            throw new ElementNotFoundException("RentCart Item");
        }

        public async Task<RentCart> GetByKey(int key1, int key2)
        {
            var rentCart = await _context.RentCart.FirstOrDefaultAsync(sc => sc.UserId == key1 && sc.BookId == key2);

            if (rentCart != null)
            {
                return rentCart;
            }

            return null;
        }

        public async Task<IEnumerable<RentCart>> GetAll()
        {
            var rentCartItems = await _context.RentCart.ToListAsync();

            if (rentCartItems.Any())
            {
                return rentCartItems;
            }

            throw new EmptyListException("RentCart");

        }

        public async Task<RentCart> Update(RentCart item)
        {
            var rentCart = await GetByKey(item.UserId, item.BookId);
            if (rentCart != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return rentCart;
            }
            throw new ElementNotFoundException("SuperCart");
        }



    }
}
