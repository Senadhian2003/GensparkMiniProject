using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class FineDetailRepository : ICompositeKeyRepository<int, FineDetail>
    {
        private readonly LibraryManagementContext _context;
        public FineDetailRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<FineDetail> Add(FineDetail item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<FineDetail> DeleteByKey(int key1, int key2)
        {
            var fineDetail = await GetByKey(key1, key2);
            if (fineDetail != null)
            {
                _context.Remove(fineDetail);
                await _context.SaveChangesAsync(true);
                return fineDetail;
            }
            throw new ElementNotFoundException("FineDetail Item");
        }

        public async Task<FineDetail> GetByKey(int key1, int key2)
        {
            var fineDetail = await _context.FineDetails.FirstOrDefaultAsync(sc => sc.RentId == key1 && sc.BookId == key2);

            if (fineDetail != null)
            {
                return fineDetail;
            }

            return null;
        }

        public async Task<IEnumerable<FineDetail>> GetAll()
        {
            var fineDetails = await _context.FineDetails.ToListAsync();

            if (fineDetails.Any())
            {
                return fineDetails;
            }

            throw new EmptyListException("FineDetail");

        }

        public async Task<FineDetail> Update(FineDetail item)
        {
            var fineDetail = await GetByKey(item.RentId, item.BookId);
            if (fineDetail != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return fineDetail;
            }
            throw new ElementNotFoundException("SuperCart");
        }



    }
}
