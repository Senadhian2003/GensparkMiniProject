using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class UserRepository : IRepository<int,User>
    {

        private readonly LibraryManagementContext _context;
        public UserRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<User> DeleteByKey(int key)
        {
            var user = await GetByKey(key);
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync(true);
                return user;
            }
            throw new ElementNotFoundException("User");
        }

        public async Task<User> GetByKey(int key)
        {
            var user = await _context.Users.Include(u=> u.CartItems).FirstOrDefaultAsync(u => u.Id == key);

            if (user != null)
            {
                return user;
            }

            throw new ElementNotFoundException("User");
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();

            if (users.Any())
            {
                return users;
            }

            throw new EmptyListException("User");

        }

        public async Task<User> Update(User item)
        {
            var user = await GetByKey(item.Id);
            if (user != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return user;
            }
            throw new ElementNotFoundException("User");
        }

    }
}
