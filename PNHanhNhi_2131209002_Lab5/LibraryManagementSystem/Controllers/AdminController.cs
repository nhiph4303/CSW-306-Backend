using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [Authorize(Roles = "ADMIN")]
        [HttpGet("dashboard")]
        public IActionResult AdminDashboard()
        {
            return Ok("Welcome, Admin!");
        }

    }
}
