using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;

namespace TaskTracker.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<User> Users {  get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<TaskDetail> TaskDetails { get; set; }


    }
}
