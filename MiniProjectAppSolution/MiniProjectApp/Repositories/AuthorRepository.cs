using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class AuthorRepository : IRepository<int, Author>
    {

        private readonly LibraryManagementContext _context;
        public AuthorRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<Author> Add(Author item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Author> DeleteByKey(int key)
        {
            var book = await GetByKey(key);
            if (book != null)
            {
                _context.Remove(book);
                await _context.SaveChangesAsync(true);
                return book;
            }
            throw new ElementNotFoundException("Author");
        }

        public async Task<Author> GetByKey(int key)
        {
            var book = await _context.Authors.FirstOrDefaultAsync(u => u.Id == key);

            if (book != null)
            {
                return book;
            }

            throw new ElementNotFoundException("Author");
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            var authors = await _context.Authors.ToListAsync();

            if (authors.Any())
            {
                return authors;
            }

            throw new EmptyListException("Author");

        }

        public async Task<Author> Update(Author item)
        {
            var book = await GetByKey(item.Id);
            if (book != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return book;
            }
            throw new ElementNotFoundException("Author");
        }


    }
}
