using Microsoft.EntityFrameworkCore;
using MiniTask_API.Data;
using MiniTask_API.Models;
using MiniTask_API.Repository;

namespace MiniTask_API.Interface
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesByUserAsync(string userId)
        {
            return await _context.categories
                .Include(c => c.Tasks)
                .Where(c => c.Tasks.Any(t => t.UserId == userId))
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id, string userId)
        {
            return await _context.categories
                .Include(c => c.Tasks)
                .FirstOrDefaultAsync(c => c.Id == id && c.Tasks.Any(t => t.UserId == userId));
        }

        public async Task CreateCategoryAsync(Category category)
        {
            _context.categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _context.categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

}
