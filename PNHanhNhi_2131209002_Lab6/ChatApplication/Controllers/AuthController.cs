
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatApplication.Models;
using ChatApplication.Service;
using ChatApplication.Dtos;

namespace ChatApplication.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWTService _jwtService;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJWTService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpGet("Login")]
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("~/Views/Account/Login.cshtml");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequestDto)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());

            if (existingUser == null)
            {
                return Unauthorized("Username or password is incorrect");
            }

            var checkPassword = await _signInManager.CheckPasswordSignInAsync(existingUser, loginRequestDto.Password, false);

            if (!checkPassword.Succeeded)
            {
                return Unauthorized("Username or password is incorrect");
            }

            var token = _jwtService.GenerateJwtToken(existingUser);

            return Ok(new { token });
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View("~/Views/Account/Register.cshtml");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto req)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data" });

            var exists = await _userManager.FindByNameAsync(req.Username);
            if (exists != null)
                return Conflict(new { message = "Username already exists" });

            var user = new AppUser
            {
                UserName = req.Username,
                Email = $"{req.Username}@local.app"
            };

            var result = await _userManager.CreateAsync(user, req.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = result.Errors.First().Description
                });
            }

            // Optional: auto sign-in
            await _signInManager.SignInAsync(user, isPersistent: false);

            var token = _jwtService.GenerateJwtToken(user);

            return Ok(new { token });
        }
    }

}
