using LibraryManagementSytem.Data;
using LibraryManagementSytem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LibraryManagementSytem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public UsersController(LibraryDbContext context)
    {
        _context = context;
    }

    // POST /api/users/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        // Check existing email
        var exists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
        if (exists)
            return BadRequest("Email already exists.");

        // Create activation code
        string activeCode = Guid.NewGuid().ToString("N");

        var user = new User
        {
            Fullname = dto.Fullname,
            Email = dto.Email,
            Password = PasswordHasher.Hash(dto.Password),
            CreatedDate = DateTime.Now,
            IsActive = false,
            IsDeleted = false,
            ActiveCode = activeCode,
            Status = 0
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // simulate email sending
        string fakeLink = $"https://localhost:7168/api/users/activate?code={activeCode}";

        return Ok(new
        {
            message = "Registered successfully! Please activate your account.",
            activateUrl = fakeLink
        });
    }

    // GET /api/users/activate?code=xxxx
    [HttpGet("activate")]
    public async Task<IActionResult> Activate(string code)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.ActiveCode == code);

        if (user == null)
            return BadRequest("Invalid activation code.");

        user.IsActive = true;
        user.ActiveCode = null;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Account activated successfully!" });
    }

    // POST /api/users/activate
    [HttpPost("activate")]
    public async Task<IActionResult> ActivatePost([FromBody] UserActivateDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.ActiveCode == dto.Code);

        if (user == null)
            return BadRequest("Invalid activation code.");

        user.IsActive = true;
        user.ActiveCode = null;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Account activated successfully!" });
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
        var username = User.Identity?.Name;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        return Ok(new { username, role });
    }

    [Authorize(Policy = "ActiveUserOnly")]
    [HttpGet("active-user")]
    public IActionResult GetActiveUserContent()
    {
        return Ok("This content is only for active users.");
    }
}
