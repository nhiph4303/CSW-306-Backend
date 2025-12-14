using LibraryManagementSytem.Data;
using LibraryManagementSytem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static LibraryManagementSytem.Data.CarouselDto;

namespace LibraryManagementSytem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarouselController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public CarouselController(LibraryDbContext context)
    {
        _context = context;
    }

    // GET: api/carousel
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _context.Carousels
            .Where(c => c.IsActive)
            .OrderBy(c => c.Order)
            .ToListAsync();

        return Ok(items);
    }

    // GET: api/carousel/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _context.Carousels.FindAsync(id);
        if (item == null) return NotFound();

        return Ok(item);
    }

    // POST: api/carousel
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CarouselCreationDto dto)
    {
        Carousel c = new()
        {
            Title = dto.Title,
            ImageUrl = dto.ImageUrl,
            Description = dto.Description,
            IsActive = dto.IsActive,
            LinkUrl = dto.LinkUrl,
            Order = dto.Order,
            UpdatedDate = DateTime.Now,
            CreatedDate = DateTime.Now
        };

        _context.Carousels.Add(c);
        await _context.SaveChangesAsync();

        return Ok(c); // return real entity
    }

    // PUT: api/carousel/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CarouselCreationDto dto)
    {
        var item = await _context.Carousels.FindAsync(id);
        if (item == null) return NotFound();

        item.Title = dto.Title;
        item.Description = dto.Description;
        item.ImageUrl = dto.ImageUrl;
        item.LinkUrl = dto.LinkUrl;
        item.Order = dto.Order;
        item.IsActive = dto.IsActive;
        item.UpdatedDate = DateTime.Now;

        await _context.SaveChangesAsync();
        return Ok(item);
    }

    // DELETE: api/carousel/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Carousels.FindAsync(id);
        if (item == null) return NotFound();

        _context.Carousels.Remove(item);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Deleted successfully" });
    }
}
