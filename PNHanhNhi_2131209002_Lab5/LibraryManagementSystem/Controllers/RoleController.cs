using LibraryManagementSystem.Data.DTO;
using LibraryManagementSytem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagementSystem.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public RoleController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/roles
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _context.Roles.Select(r=> new { 
                roleId = r.RoleId,
                roleName = r.RoleName,
                description = r.Description
            }).ToListAsync();
            return Ok(roles);
        }

        // GET api/roles/<role name>
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var role = await _context.Roles.Select(r => new {
                roleId = r.RoleId,
                roleName = r.RoleName,
                description = r.Description
            }).FirstOrDefaultAsync(r => r.roleName.Equals(name));

            if (role == null)
            {
                return BadRequest("Role not found");
            }

            return Ok(role);
        }

        // POST api/roles
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleCreateDto dto)
        {
            var existingRole = await _context.Roles.AnyAsync(r => r.RoleName.Equals(dto.RoleName));
            if (existingRole)
            {
                return BadRequest("Role already exists");
            }

            var newRole = new Models.Role
            {
                RoleName = dto.RoleName,
                Description = dto.Description
            };
            _context.Roles.Add(newRole);
            await _context.SaveChangesAsync();
            return Ok(newRole);
        }


        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound("Role not found");
            }
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
            return Ok($"Role ID:{role.RoleId} was succesfully deleted");
        }
    }
}
