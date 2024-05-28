using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class PurchaseRepository : IRepository<int,Purchase>
    {
        private readonly LibraryManagementContext _context;
        public PurchaseRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<Purchase> Add(Purchase item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Purchase> DeleteByKey(int key)
        {
            var purchase = await GetByKey(key);
            if (purchase != null)
            {
                _context.Remove(purchase);
                await _context.SaveChangesAsync(true);
                return purchase;
            }
            throw new ElementNotFoundException("Purchase");
        }

        public async Task<Purchase> GetByKey(int key)
        {
            var purchase = await _context.Purchases.Include(p => p.PurchaseDetailsList ).ThenInclude(pd => pd.Book).FirstOrDefaultAsync(p => p.PurchaseId == key);

            if (purchase != null)
            {
                return purchase;
            }

            throw new ElementNotFoundException("Purchase");
        }

        public async Task<IEnumerable<Purchase>> GetAll()
        {
            var purchases = await _context.Purchases.ToListAsync();

            if (purchases.Any())
            {
                return purchases;
            }

            throw new EmptyListException("Purchase");

        }

        public async Task<Purchase> Update(Purchase item)
        {
            var purchase = await GetByKey(item.PurchaseId);
            if (purchase != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return purchase;
            }
            throw new ElementNotFoundException("Purchase");
        }





    }
}
