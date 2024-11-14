using Microsoft.AspNetCore.Identity;

namespace MiniTask_API.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public bool IsCompleted { get; set; }

        // Foreign Key to Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Foreign Key to User
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }

}