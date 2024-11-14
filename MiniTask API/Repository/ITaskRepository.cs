using MiniTask_API.Models;

namespace MiniTask_API.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetTasksByUserAsync(string userId);
        Task<TaskItem> GetTaskByIdAsync(int id, string userId);
        Task CreateTaskAsync(TaskItem task);
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(TaskItem task);
    }

}
