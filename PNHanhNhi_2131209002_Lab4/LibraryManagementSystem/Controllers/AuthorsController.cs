using LibraryManagementSytem.Data;
using LibraryManagementSytem.Data.DTO;
using LibraryManagementSytem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSytem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly LibraryDbContext _context;
    private readonly IWebHostEnvironment _env;

    public AuthorsController(LibraryDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET api/authors?search=abc
    [HttpGet]
    public async Task<IActionResult> GetAuthors(string? search = null)
    {
        var query = _context.Authors
            .Where(a => !a.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
            query = query.Where(a =>
                a.FirstName.Contains(search) ||
                a.LastName.Contains(search));

        var authors = await query.ToListAsync();

        return Ok(authors);
    }

    // GET api/authors/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.AuthorId == id && !a.IsDeleted);

        if (author == null) return NotFound();

        return Ok(author);
    }

    // POST api/authors
    [HttpPost]
    public async Task<IActionResult> CreateAuthor(
        [FromForm] AuthorCreateDto dto,
        IFormFile? avatar)
    {
        var author = new Author
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Biography = dto.Biography,
            Nationality = dto.Nationality,
            Email = dto.Email,
            Website = dto.Website,
            CreatedDate = DateTime.Now,
            IsActive = true
        };

        // Save avatar
        if (avatar != null)
        {
            string folder = Path.Combine("wwwroot/author_avatars");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, avatar.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await avatar.CopyToAsync(stream);

            author.Avatar = "/author_avatars/" + avatar.FileName;
        }

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return Ok(author);
    }

    // PUT api/authors/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(
        int id,
        [FromForm] AuthorUpdateDto dto,
        IFormFile? avatar)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return NotFound();

        author.FirstName = dto.FirstName;
        author.LastName = dto.LastName;
        author.DateOfBirth = dto.DateOfBirth;
        author.Biography = dto.Biography;
        author.Nationality = dto.Nationality;
        author.Email = dto.Email;
        author.Website = dto.Website;
        author.IsActive = dto.IsActive;

        // Update avatar
        if (avatar != null)
        {
            string folder = Path.Combine("wwwroot/author_avatars");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, avatar.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await avatar.CopyToAsync(stream);

            author.Avatar = "/author_avatars/" + avatar.FileName;
        }

        await _context.SaveChangesAsync();
        return Ok(author);
    }

    // DELETE api/authors/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return NotFound();

        author.IsDeleted = true;
        author.IsActive = false;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Author soft deleted" });
    }
}
