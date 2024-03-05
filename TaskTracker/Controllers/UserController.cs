using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskTracker.DTO;
using TaskTracker.Models;
using TaskTracker.Repositories;
using TaskTracker.Services;


namespace TaskTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticateService _authenticateService;

        public UserController(IUserRepository userRepository,IAuthenticateService authenticateService)
        {
            _userRepository = userRepository;
            _authenticateService = authenticateService;
        }

         [HttpPost("register")]
         public async Task<ActionResult> CreateUser(User user)
         {
             try
             {
                 var createdUser = await _userRepository.CreateUserAsync(user);
                 return Ok(createdUser);
             }
             catch (Exception ex)
             {
                 return StatusCode(500, ex.Message);
             }
         }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _userRepository.ValidateUserCredentials(loginDto.UserName, loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }
            var token = _authenticateService.GenerateJwtToken(loginDto);
            return Ok(new { Token = token });
        }

        [HttpGet("gettasks")]
        [Authorize]
        public async Task<ActionResult> GetTasks()
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var taskdetails=await _userRepository.GetTaskDetailsAsync(int.Parse(userId));
                return Ok(taskdetails);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("createtasks")]
        [Authorize]
        public async Task<ActionResult> CreateTasks([FromBody] TaskCreationDto taskCreation)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var createdTasks = await _userRepository.CreateTaskAsync(int.Parse(userId), taskCreation);
                return Ok(createdTasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("updatetask/{taskId}")]
        [Authorize]
        public async Task<ActionResult> UpdateTaskStatus([FromRoute] int taskId, [FromBody] TaskCreationDto updatedTask)
        {
            try
            {
                bool updated = await _userRepository.UpdateTaskAsync(taskId, updatedTask);
                if (updated)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deletetask")]
        [Authorize]
        public async Task<ActionResult> DeleteTasks([FromRoute]int taskId)
        {
            try
            {
                bool deletedTask=await _userRepository.DeleteTaskAsync(taskId);
                if(!deletedTask) 
                {
                    return NotFound();
                }
                return Ok();    
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("statistics")]   
        public async Task<ActionResult> GetTaskStatistics()
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var statistics = await _userRepository.GetTaskStatisticsAsync(int.Parse(userId));
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
