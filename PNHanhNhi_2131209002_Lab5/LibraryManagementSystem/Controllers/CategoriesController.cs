using LibraryManagementSytem.Data;
using LibraryManagementSytem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSytem.Controllers;

[Authorize(Policy = "CanManageCategories")]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly LibraryDbContext _context;
    private readonly IWebHostEnvironment _env;

    public CategoriesController(LibraryDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET /api/categories?search=abc&isActive=true
    [HttpGet]
    public async Task<IActionResult> GetCategories(string? search = null, bool? isActive = null)
    {
        var query = _context.Categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => c.Name.Contains(search));

        if (isActive != null)
            query = query.Where(c => c.IsActive == isActive);

        var items = await query
            .OrderBy(c => c.Name)
            .Select(c => new
            {
                c.CategoryId,
                c.Name,
                c.Description,
                c.CreatedDate,
                c.UpdatedDate,
                c.IsActive,
                c.Avatar
            })
            .ToListAsync();

        return Ok(items);
    }

    // GET /api/categories/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var c = await _context.Categories
            .Where(x => x.CategoryId == id)
            .Select(x => new
            {
                x.CategoryId,
                x.Name,
                x.Description,
                x.CreatedDate,
                x.UpdatedDate,
                x.IsActive,
                x.Avatar
            })
            .FirstOrDefaultAsync();

        if (c == null) return NotFound();
        return Ok(c);
    }

    // POST — create category with optional avatar
    [HttpPost]
    public async Task<IActionResult> CreateCategory(
        [FromForm] CategoryCreateDto dto,
        IFormFile? avatar)
    {
        var cat = new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedDate = DateTime.Now,
            IsActive = true
        };

        // Save avatar
        if (avatar != null)
        {
            var folder = Path.Combine("wwwroot/category_images");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, avatar.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await avatar.CopyToAsync(stream);

            cat.Avatar = "/category_images/" + avatar.FileName;
        }

        _context.Categories.Add(cat);
        await _context.SaveChangesAsync();

        return Ok(cat);
    }

    // PUT — update categories
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(
        int id,
        [FromForm] CategoryUpdateDto dto,
        IFormFile? avatar)
    {
        var cat = await _context.Categories.FindAsync(id);
        if (cat == null) return NotFound();

        cat.Name = dto.Name;
        cat.Description = dto.Description;
        cat.IsActive = dto.IsActive;
        cat.UpdatedDate = DateTime.Now;

        // avatar update
        if (avatar != null)
        {
            var folder = Path.Combine("wwwroot/category_images");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, avatar.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await avatar.CopyToAsync(stream);

            cat.Avatar = "/category_images/" + avatar.FileName;
        }

        await _context.SaveChangesAsync();
        return Ok(cat);
    }

    // PATCH toggle active
    [HttpPatch("{id}/toggle")]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var cat = await _context.Categories.FindAsync(id);
        if (cat == null) return NotFound();

        cat.IsActive = !cat.IsActive;
        cat.UpdatedDate = DateTime.Now;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Toggled successfully",
            newStatus = cat.IsActive
        });
    }
}
