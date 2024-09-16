using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class FineRepository : IRepository<int, Fine>
    {

        private readonly LibraryManagementContext _context;
        public FineRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<Fine> Add(Fine item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Fine> DeleteByKey(int key)
        {
            var fine = await GetByKey(key);
            if (fine != null)
            {
                _context.Remove(fine);
                await _context.SaveChangesAsync(true);
                return fine;
            }
            throw new ElementNotFoundException("Fine");
        }

        public async Task<Fine> GetByKey(int key)
        {
            var fine = await _context.Fines.FirstOrDefaultAsync(f => f.FineId == key);

            if (fine != null)
            {
                return fine;
            }

            throw new ElementNotFoundException("Fine");
        }

        public async Task<IEnumerable<Fine>> GetAll()
        {
            var fines = await _context.Fines.Include(f=>f.User).Include(f=>f.FineDetailsList).ThenInclude(fd=>fd.Book).ToListAsync();

            if (fines.Any())
            {
                return fines;
            }

            return fines;

        }

        public async Task<Fine> Update(Fine item)
        {
            var fine = await GetByKey(item.FineId);
            if (fine != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return fine;
            }
            throw new ElementNotFoundException("Fine");
        }





    }
}
