using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class SuperRentCartRepository : ICompositeKeyRepository<int,SuperRentCart>
    {
        private readonly LibraryManagementContext _context;
        public SuperRentCartRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<SuperRentCart> Add(SuperRentCart item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<SuperRentCart> DeleteByKey(int key1, int key2)
        {
            var superRentCart = await GetByKey(key1, key2);
            if (superRentCart != null)
            {
                _context.Remove(superRentCart);
                await _context.SaveChangesAsync(true);
                return superRentCart;
            }
            throw new ElementNotFoundException("SuperRentCart Item");
        }

        public async Task<SuperRentCart> GetByKey(int key1, int key2)
        {
            var superRentCart = await _context.SuperRentCart.FirstOrDefaultAsync(sc => sc.UserId == key1 && sc.BookId == key2);

            if (superRentCart != null)
            {
                return superRentCart;
            }

            return null;
        }

        public async Task<IEnumerable<SuperRentCart>> GetAll()
        {
            var rentCartItems = await _context.SuperRentCart.ToListAsync();

            if (rentCartItems.Any())
            {
                return rentCartItems;
            }

            throw new EmptyListException("SuperRentCart");

        }

        public async Task<SuperRentCart> Update(SuperRentCart item)
        {
            var superRentCart = await GetByKey(item.UserId, item.BookId);
            if (superRentCart != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return superRentCart;
            }
            throw new ElementNotFoundException("SuperCart");
        }




    }
}
