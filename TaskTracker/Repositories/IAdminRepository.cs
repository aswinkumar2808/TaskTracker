using TaskTracker.DTO;
using TaskTracker.Models;

namespace TaskTracker.Repositories
{
    public interface IAdminRepository
    {
        LoginDto ValidateAdminCredentials(string username, string password);
        Task<List<TaskDetail>> GetAllTasksAsync();
        Task<List<User>> GetAllUsersAsync();
    }
}
