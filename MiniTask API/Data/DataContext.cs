using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniTask_API.Models;

namespace MiniTask_API.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<TaskItem> taskItems { get; set; }
        public DbSet<Category> categories { get; set; }

    }
}
