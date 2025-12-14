using LeThiMinhTuoi_Lab6_Backend.Dtos;
using LeThiMinhTuoi_Lab6_Backend.Interfaces;
using LeThiMinhTuoi_Lab6_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeThiMinhTuoi_Lab6_Backend.Controllers
{
        [Route("Account")]
        [ApiController]
        public class AuthController : Controller
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly ITokenService _tokenService;

            public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _tokenService = tokenService;
            }

            [HttpGet("Login")]
            [HttpGet("")]
            public IActionResult Index()
            {
                return View("~/Views/Account/Login.cshtml");
            }

        [HttpPost("Login")]
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

                var token = _tokenService.GenerateJwtToken(existingUser);

                return Ok(new { token });
            }
        }

}
