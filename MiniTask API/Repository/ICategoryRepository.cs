using MiniTask_API.Models;

namespace MiniTask_API.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesByUserAsync(string userId);
        Task<Category> GetCategoryByIdAsync(int id, string userId);
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
    }

}
