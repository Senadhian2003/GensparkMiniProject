using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class BookRepository : IRepository<int, Book>
    {
        private readonly LibraryManagementContext _context;
        public BookRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<Book> Add(Book item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Book> DeleteByKey(int key)
        {
            var book = await GetByKey(key);
            if (book != null)
            {
                _context.Remove(book);
                await _context.SaveChangesAsync(true);
                return book;
            }
            throw new ElementNotFoundException("Book");
        }

        public async Task<Book> GetByKey(int key)
        {
            var book = await _context.Books.FirstOrDefaultAsync(u => u.Id == key);

            if (book != null)
            {
                return book;
            }

            throw new ElementNotFoundException("Book");
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var users = await _context.Books.ToListAsync();

            if (users.Any())
            {
                return users;
            }

            throw new EmptyListException("Book");

        }

        public async Task<Book> Update(Book item)
        {
            var book = await GetByKey(item.Id);
            if (book != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return book;
            }
            throw new ElementNotFoundException("Book");
        }


    }
}
