using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class PublisherRepository : IRepository<int, Publisher>
    {

        private readonly LibraryManagementContext _context;
        public PublisherRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<Publisher> Add(Publisher item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Publisher> DeleteByKey(int key)
        {
            var publisher = await GetByKey(key);
            if (publisher != null)
            {
                _context.Remove(publisher);
                await _context.SaveChangesAsync(true);
                return publisher;
            }
            throw new ElementNotFoundException("Publisher");
        }

        public async Task<Publisher> GetByKey(int key)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(u => u.Id == key);

            if (publisher != null)
            {
                return publisher;
            }

            throw new ElementNotFoundException("Publisher");
        }

        public async Task<IEnumerable<Publisher>> GetAll()
        {
            var publishers = await _context.Publishers.ToListAsync();

            if (publishers.Any())
            {
                return publishers;
            }

            throw new EmptyListException("Publisher");

        }

        public async Task<Publisher> Update(Publisher item)
        {
            var publisher = await GetByKey(item.Id);
            if (publisher != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return publisher;
            }
            throw new ElementNotFoundException("Publisher");
        }


    }
}
