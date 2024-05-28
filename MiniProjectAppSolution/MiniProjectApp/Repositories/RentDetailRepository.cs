using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class RentDetailRepository : ICompositeKeyRepository<int, RentDetail>
    {

        private readonly LibraryManagementContext _context;
        public RentDetailRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<RentDetail> Add(RentDetail item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<RentDetail> DeleteByKey(int key1, int key2)
        {
            var rentDetail = await GetByKey(key1, key2);
            if (rentDetail != null)
            {
                _context.Remove(rentDetail);
                await _context.SaveChangesAsync(true);
                return rentDetail;
            }
            throw new ElementNotFoundException("RentDetail Item");
        }

        public async Task<RentDetail> GetByKey(int key1, int key2)
        {
            var rentDetail = await _context.RentDetails.FirstOrDefaultAsync(sc => sc.RentId == key1 && sc.BookId == key2);

            if (rentDetail != null)
            {
                return rentDetail;
            }

            return null;
        }

        public async Task<IEnumerable<RentDetail>> GetAll()
        {
            var rentDetails = await _context.RentDetails.ToListAsync();

            if (rentDetails.Any())
            {
                return rentDetails;
            }

            throw new EmptyListException("RentDetail");

        }

        public async Task<RentDetail> Update(RentDetail item)
        {
            var rentDetail = await GetByKey(item.RentId, item.BookId);
            if (rentDetail != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return rentDetail;
            }
            throw new ElementNotFoundException("SuperCart");
        }


    }
}
