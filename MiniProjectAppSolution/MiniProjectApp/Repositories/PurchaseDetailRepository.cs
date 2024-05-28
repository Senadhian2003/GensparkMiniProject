using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class PurchaseDetailRepository : ICompositeKeyRepository<int, PurchaseDetail>
    {

        private readonly LibraryManagementContext _context;
        public PurchaseDetailRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<PurchaseDetail> Add(PurchaseDetail item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<PurchaseDetail> DeleteByKey(int key1, int key2)
        {
            var purchaseDetail = await GetByKey(key1, key2);
            if (purchaseDetail != null)
            {
                _context.Remove(purchaseDetail);
                await _context.SaveChangesAsync(true);
                return purchaseDetail;
            }
            throw new ElementNotFoundException("PurchaseDetail Item");
        }

        public async Task<PurchaseDetail> GetByKey(int key1, int key2)
        {
            var purchaseDetail = await _context.PurchaseDetails.FirstOrDefaultAsync(pd => pd.PurchaseId == key1 && pd.BookId == key2);

            if (purchaseDetail != null)
            {
                return purchaseDetail;
            }

            return null;
        }

        public async Task<IEnumerable<PurchaseDetail>> GetAll()
        {
            var purchaseDetails = await _context.PurchaseDetails.ToListAsync();

            if (purchaseDetails.Any())
            {
                return purchaseDetails;
            }

            throw new EmptyListException("PurchaseDetail");

        }

        public async Task<PurchaseDetail> Update(PurchaseDetail item)
        {
            var purchaseDetail = await GetByKey(item.PurchaseId, item.BookId);
            if (purchaseDetail != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return purchaseDetail;
            }
            throw new ElementNotFoundException("SuperCart");
        }



    }
}
