using Azure.Core;
using LibraryManagementSystem.Data.DTO;
using LibraryManagementSystem.Helpers;
using LibraryManagementSytem.Data;
using LibraryManagementSytem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace LibraryManagementSystem.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly JwtHelpers _jwtHelpers;
        public AuthController(LibraryDbContext context, IConfiguration configuration, JwtHelpers jwtHelpers)
        {
            _context = context;
            _configuration = configuration;
            _jwtHelpers = jwtHelpers;
        }


        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var hashedPassword = PasswordHasher.Hash(request.Password);

            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u =>
                u.Username == request.Username && u.Password == hashedPassword);
            if (user == null)
                return Unauthorized();

            var token = _jwtHelpers.GenerateJwtToken(user);

            var expiryMinutes = _configuration.GetValue<int>("JwtSettings:ExpiryMinutes");


            return Ok(new { token, expriresIn = (int)(expiryMinutes * 60) });

        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {

            var userValidations = await _context.Users.Select(u => new
            {
                u.Username,
                u.UserCode
            }).ToListAsync();

            // Check existing username
            if (userValidations.Any(u => u.Username.Equals(request.Username)))
            {
                return BadRequest("Username already exists.");
            }

            // Generate unique user code
            string userCode = Generator.GenerateUserCode();
            foreach (var existingUser in userValidations)
            {
                if (existingUser.UserCode == userCode)
                {
                    userCode = Generator.GenerateUserCode();
                }
            }

            var user = new User
            {
                Fullname = request.Fullname,
                Email = request.Email,
                Username = request.Username,
                Password = PasswordHasher.Hash(request.Password),
                CreatedDate = DateTime.Now,
                UserCode = userCode,
                IsActive = false,
                IsDeleted = false
            };

            // add role "User" to new registered user
            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "USER");
            if (userRole != null)
            {
                user.Roles.Add(userRole);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = await _context.Users
                .Where(u => u.Username == request.Username)
                .Select(u => new
                {
                    u.UserId,
                    u.Username,
                    u.UserCode,
                    u.Fullname,
                    u.Email,
                    u.Phone,
                    u.CreatedDate,
                    u.IsActive,
                    u.Status,
                    Roles = u.Roles.Select(r => r.RoleName).ToList()
                })
                .FirstOrDefaultAsync();

            return Created(string.Empty, response);
        }


    }
}
