using LibraryManagementSystem.Models;
using LibraryManagementSytem.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementSystem.Helpers
{
    public class JwtHelpers
    {
        private readonly IConfiguration _configuration;

        public JwtHelpers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                // JWT STANDARD
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("is_active", user.IsActive.ToString()),
                new Claim("register_at", user.CreatedDate.ToString("o")),
                new Claim("email_confirmed", user.EmailConfirmed.ToString())
            };

            bool canMangeCategories = false;

            // Roles
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim("role", role.RoleName));
                if (role.RoleName == "ADMIN")
                {
                    canMangeCategories = true;
                }
            }

            // just allow ADMIN to manage categories
            claims.Add(new("can_manage_categories", canMangeCategories.ToString()));


            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _configuration.GetValue<int>("JwtSettings:ExpiryMinutes")
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
