using Microsoft.EntityFrameworkCore;
using MiniTask_API.Data;
using MiniTask_API.Models;
using MiniTask_API.Repository;

namespace MiniTask_API.Interface
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByUserAsync(string userId)
        {
            return await _context.taskItems.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id, string userId)
        {
            return await _context.taskItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task CreateTaskAsync(TaskItem task)
        {
            _context.taskItems.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TaskItem task)
        {
            _context.taskItems.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

}
