using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Context;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class FeedbackRepository : IRepository<int, Feedback>
    {

        private readonly LibraryManagementContext _context;
        public FeedbackRepository(LibraryManagementContext context)
        {
            _context = context;
        }
        public async Task<Feedback> Add(Feedback item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Feedback> DeleteByKey(int key)
        {
            var feedback = await GetByKey(key);
            if (feedback != null)
            {
                _context.Remove(feedback);
                await _context.SaveChangesAsync(true);
                return feedback;
            }
            throw new ElementNotFoundException("Feedback");
        }

        public async Task<Feedback> GetByKey(int key)
        {
            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(u => u.UserId == key);

            if (feedback != null)
            {
                return feedback;
            }

            throw new ElementNotFoundException("Feedback");
        }

        public async Task<IEnumerable<Feedback>> GetAll()
        {
            var feedbacks = await _context.Feedbacks.ToListAsync();

            if (feedbacks.Any())
            {
                return feedbacks;
            }

            throw new EmptyListException("Feedback");

        }

        public async Task<Feedback> Update(Feedback item)
        {
            var feedback = await GetByKey(item.UserId);
            if (feedback != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return feedback;
            }
            throw new ElementNotFoundException("Feedback");
        }



    }
}
