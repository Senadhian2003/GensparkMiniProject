using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class UserCredentialRepository : IRepository<int,UserCredential>
    {

        private readonly LibraryManagementContext _context;
        public UserCredentialRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<UserCredential> Add(UserCredential item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<UserCredential> DeleteByKey(int key)
        {
            var userCredentials = await GetByKey(key);
            if (userCredentials != null)
            {
                _context.Remove(userCredentials);
                await _context.SaveChangesAsync(true);
                return userCredentials;
            }
            throw new ElementNotFoundException("UserCredential");
        }

        public async Task<UserCredential> GetByKey(int key)
        {
            var userCredentials = await _context.UserCredentials.FirstOrDefaultAsync(u => u.UserId == key);

            if (userCredentials != null)
            {
                return userCredentials;
            }

            throw new ElementNotFoundException("UserCredential");
        }

        public async Task<IEnumerable<UserCredential>> GetAll()
        {
            var users = await _context.UserCredentials.ToListAsync();

            if (users.Any())
            {
                return users;
            }

            throw new EmptyListException("UserCredential");

        }

        public async Task<UserCredential> Update(UserCredential item)
        {
            var userCredentials = await GetByKey(item.UserId);
            if (userCredentials != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return userCredentials;
            }
            throw new ElementNotFoundException("UserCredential");
        }


    }
}
