using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.DTO;
using TaskTracker.Models;

namespace TaskTracker.Repositories
{
    public class AdminRepository:IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AdminRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public LoginDto ValidateAdminCredentials(string username, string password)
        {
            var user = _dbContext.Admins.FirstOrDefault(u => u.Name == username && u.Password == password);
            if (user == null)
            {
                return null;
            }
            var loginDto = new LoginDto
            {

                UserName = user.Name,
                Password = user.Password,

            };
            return loginDto;
        }

        public async Task<List<TaskDetail>> GetAllTasksAsync()
        {
            var allTasks = await _dbContext.TaskDetails.ToListAsync();
            return allTasks;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var allUsers = await _dbContext.Users.ToListAsync();
            return allUsers;
        }
    }
}
