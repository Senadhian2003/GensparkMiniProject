using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class RentRepository : IRepository<int,Rent>
    {

        private readonly LibraryManagementContext _context;
        public RentRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<Rent> Add(Rent item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Rent> DeleteByKey(int key)
        {
            var rent = await GetByKey(key);
            if (rent != null)
            {
                _context.Remove(rent);
                await _context.SaveChangesAsync(true);
                return rent;
            }
            throw new ElementNotFoundException("Rent");
        }

        public async Task<Rent> GetByKey(int key)
        {
            var rent = await _context.Rents.Include(r=>r.RentDetailsList).FirstOrDefaultAsync(r => r.RentId == key);

            if (rent != null)
            {
                return rent;
            }

            throw new ElementNotFoundException("Rent");
        }

        public async Task<IEnumerable<Rent>> GetAll()
        {
            var rents = await _context.Rents.Include(r=>r.RentDetailsList).ToListAsync();

            if (rents.Any())
            {
                return rents;
            }

            return rents;

        }

        public async Task<Rent> Update(Rent item)
        {
            var rent = await GetByKey(item.RentId);
            if (rent != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return rent;
            }
            throw new ElementNotFoundException("Rent");
        }



    }
}
