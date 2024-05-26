using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class SuperCartRepository : ICompositeKeyRepository<int,SuperCart>
    {

        private readonly LibraryManagementContext _context;
        public SuperCartRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<SuperCart> Add(SuperCart item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<SuperCart> DeleteByKey(int key1, int key2)
        {
            var superCart = await GetByKey(key1, key2);
            if (superCart != null)
            {
                _context.Remove(superCart);
                await _context.SaveChangesAsync(true);
                return superCart;
            }
            throw new ElementNotFoundException("SpuerCart");
        }

        public async Task<SuperCart> GetByKey(int key1, int key2)
        {
            var superCart = await _context.SuperCarts.FirstOrDefaultAsync(sc => sc.UserId == key1 && sc.BookId == key2);

            if (superCart != null)
            {
                return superCart;
            }

            throw new ElementNotFoundException("SupertCart");
        }

        public async Task<IEnumerable<SuperCart>> GetAll()
        {
            var superCartItems = await _context.SuperCarts.ToListAsync();

            if (superCartItems.Any())
            {
                return superCartItems;
            }

            throw new EmptyListException("User");

        }

        public async Task<SuperCart> Update(SuperCart item)
        {
            var user = await GetByKey(item.UserId, item.BookId);
            if (user != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return user;
            }
            throw new ElementNotFoundException("SuperCart");
        }


    }
}
