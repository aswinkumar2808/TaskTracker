using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskTracker.Data;
using TaskTracker.DTO;
using TaskTracker.Models;
namespace TaskTracker.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext; 
        public AuthenticateService(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }
        public string GenerateJwtToken(LoginDto loginDto)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.UserName == loginDto.UserName);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, loginDto.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            };

            var token = new JwtSecurityToken(
                 _configuration["Jwt:Issuer"],
                 _configuration["Jwt:Audience"],
                 claims,
                 expires: DateTime.Now.AddMinutes(30),
                 signingCredentials: credentials
             );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}



