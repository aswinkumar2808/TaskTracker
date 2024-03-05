using TaskTracker.DTO;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public interface IAuthenticateService
    {
        string GenerateJwtToken(LoginDto loginDto);
    }
}


