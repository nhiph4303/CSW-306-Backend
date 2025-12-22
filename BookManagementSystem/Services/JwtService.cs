using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookManagementSystem.Models;
using Microsoft.IdentityModel.Tokens;

namespace BookManagementSystem.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                // JWT STANDARD
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("role",user.Role )
            };


            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _configuration.GetValue<int>("Jwt:ExpireMinutes")
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
