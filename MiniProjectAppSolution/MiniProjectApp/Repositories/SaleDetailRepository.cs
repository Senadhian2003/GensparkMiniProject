using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class SaleDetailRepository : ICompositeKeyRepository<int,SaleDetail>
    {
        private readonly LibraryManagementContext _context;
        public SaleDetailRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<SaleDetail> Add(SaleDetail item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<SaleDetail> DeleteByKey(int key1, int key2)
        {
            var saleDetail = await GetByKey(key1, key2);
            if (saleDetail != null)
            {
                _context.Remove(saleDetail);
                await _context.SaveChangesAsync(true);
                return saleDetail;
            }
            throw new ElementNotFoundException("SaleDetail Item");
        }

        public async Task<SaleDetail> GetByKey(int key1, int key2)
        {
            var saleDetail = await _context.SaleDetails.FirstOrDefaultAsync(sc => sc.SaleId == key1 && sc.BookId == key2);

            if (saleDetail != null)
            {
                return saleDetail;
            }

            return null;
        }

        public async Task<IEnumerable<SaleDetail>> GetAll()
        {
            var saleDetails = await _context.SaleDetails.ToListAsync();

            if (saleDetails.Any())
            {
                return saleDetails;
            }

            throw new EmptyListException("SaleDetail");

        }

        public async Task<SaleDetail> Update(SaleDetail item)
        {
            var saleDetail = await GetByKey(item.SaleId, item.BookId);
            if (saleDetail != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return saleDetail;
            }
            throw new ElementNotFoundException("SuperCart");
        }


    }
}
