using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TaskTracker.DTO;
using TaskTracker.Models;

namespace TaskTracker.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<LoginDto> ValidateUserCredentials(string username, string password);
        Task<TaskCreationDto> CreateTaskAsync(int userId, TaskCreationDto taskCreation);
        Task<IEnumerable<TaskDetail>> GetTaskDetailsAsync(int userId);
        Task<bool> UpdateTaskAsync(int taskId, TaskCreationDto updatedTask);
        Task<bool> DeleteTaskAsync(int taskId);
        Task<TaskStatisticsDto> GetTaskStatisticsAsync(int userId);
    }
}
