using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.DTO;
using TaskTracker.Repositories;
using TaskTracker.Services;

namespace TaskTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        private readonly IAuthenticateService _authenticateService;

        public AdminController(IAdminRepository adminRepository, IAuthenticateService authenticateService)
        {
            _adminRepository = adminRepository;
            _authenticateService = authenticateService;

        }

        [HttpPost("adminlogin")]

        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var admin = _adminRepository.ValidateAdminCredentials(loginDto.UserName, loginDto.Password);

            if (admin == null)
            {
                return Unauthorized("Invalid admin name or password");
            }

            var token = _authenticateService.GenerateJwtToken(admin);


            return Ok(new { Token = token });
        }

        [HttpGet("alltasks")]
        [Authorize]
        public async Task<ActionResult> GetAllTasks()
        {
            try
            {
                var allTasks = await _adminRepository.GetAllTasksAsync();
                return Ok(allTasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("allusers")]
        [Authorize]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                var allUsers = await _adminRepository.GetAllUsersAsync();
                return Ok(allUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
