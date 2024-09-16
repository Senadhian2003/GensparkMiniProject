using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class CartRepository : ICompositeKeyRepository<int,Cart>
    {

        private readonly LibraryManagementContext _context;
        public CartRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<Cart> Add(Cart item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Cart> DeleteByKey(int key1, int key2)
        {
            var superCart = await GetByKey(key1, key2);
            if (superCart != null)
            {
                _context.Remove(superCart);
                await _context.SaveChangesAsync(true);
                return superCart;
            }
            throw new ElementNotFoundException("Cart Item");
        }

        public async Task<Cart> GetByKey(int key1, int key2)
        {
            var superCart = await _context.Cart.FirstOrDefaultAsync(sc => sc.UserId == key1 && sc.BookId == key2);

            if (superCart != null)
            {
                return superCart;
            }

            return null;
        }

        public async Task<IEnumerable<Cart>> GetAll()
        {
            var superCartItems = await _context.Cart.Include(c=>c.Book).ThenInclude(b=>b.Author).ToListAsync();

            if (superCartItems.Any())
            {
                return superCartItems;
            }

            throw new EmptyListException("Cart");

        }

        public async Task<Cart> Update(Cart item)
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
