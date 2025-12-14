using LibraryManagementSytem.Data;
using LibraryManagementSytem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSytem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibraryDbContext _context;
    private readonly IWebHostEnvironment _env;

    public BooksController(LibraryDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // =======================================================================
    // GET LIST WITH SEARCH + FILTER + PAGINATION
    // =======================================================================
    [HttpGet]
    public async Task<IActionResult> GetBooks(
        int page = 1,
        int pageSize = 10,
        string? search = null,
        int? categoryId = null,
        int? authorId = null)
    {
        var query = _context.Books
            .Include(b => b.Category)
            .Include(b => b.Author)
            .Where(b => !b.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
            query = query.Where(b => b.Title.Contains(search));

        if (categoryId.HasValue)
            query = query.Where(b => b.CategoryId == categoryId);

        if (authorId.HasValue)
            query = query.Where(b => b.AuthorId == authorId);

        var totalItems = await query.CountAsync();

        var data = await query
            .OrderBy(b => b.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(b => new BookUpdateDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Description = b.Description,
                BookCode = b.BookCode,
                Publisher = b.Publisher,
                PublishedYear = b.PublishedYear,
                CategoryId = b.CategoryId,
                CategoryName = b.Category.Name,
                AuthorId = b.AuthorId,
                AuthorName = b.Author.FirstName + " " + b.Author.LastName,
                TotalCopies = b.TotalCopies,
                AvailableCopies = b.AvailableCopies,
                Avatar = b.Avatar,
                Pdf = b.Pdf,
                IsActive = b.IsActive
            })
            .ToListAsync();

        return Ok(new { totalItems, page, pageSize, data });
    }

    // =======================================================================
    // GET BOOK BY ID
    // =======================================================================
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var b = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.BookId == id && !b.IsDeleted);

        if (b == null) return NotFound();

        var dto = new BookUpdateDto
        {
            BookId = b.BookId,
            Title = b.Title,
            Description = b.Description,
            BookCode = b.BookCode,
            Publisher = b.Publisher,
            PublishedYear = b.PublishedYear,
            CategoryId = b.CategoryId,
            CategoryName = b.Category.Name,
            AuthorId = b.AuthorId,
            AuthorName = b.Author.FirstName + " " + b.Author.LastName,
            TotalCopies = b.TotalCopies,
            AvailableCopies = b.AvailableCopies,
            Avatar = b.Avatar,
            Pdf = b.Pdf,
            IsActive = b.IsActive
        };

        return Ok(dto);
    }

    // =======================================================================
    // CREATE BOOK (Upload Avatar + PDF)
    // =======================================================================
    [HttpPost]
    public async Task<IActionResult> CreateBook(
        [FromForm] BookUpdateDto dto,
        IFormFile? avatar,
        IFormFile? pdfFile)
    {
        var book = new Book
        {
            Title = dto.Title,
            Description = dto.Description,
            BookCode = dto.BookCode,
            Publisher = dto.Publisher,
            PublishedYear = dto.PublishedYear,
            CategoryId = dto.CategoryId,
            AuthorId = dto.AuthorId,
            TotalCopies = dto.TotalCopies,
            AvailableCopies = dto.TotalCopies,
            CreatedDate = DateTime.Now,
            IsActive = true
        };

        // Upload image
        if (avatar != null)
        {
            string folder = Path.Combine(_env.WebRootPath, "book_images");
            Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, avatar.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await avatar.CopyToAsync(stream);

            book.Avatar = "/book_images/" + avatar.FileName;
        }

        // Upload PDF
        if (pdfFile != null)
        {
            string folder = Path.Combine(_env.WebRootPath, "book_pdf");
            Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, pdfFile.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await pdfFile.CopyToAsync(stream);

            book.Pdf = "/book_pdf/" + pdfFile.FileName;
        }

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return Ok(book);
    }

    // =======================================================================
    // UPDATE BOOK
    // =======================================================================
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(
        int id,
        [FromForm] BookUpdateDto dto,
        IFormFile? avatar,
        IFormFile? pdfFile)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();

        book.Title = dto.Title;
        book.Description = dto.Description;
        book.BookCode = dto.BookCode;
        book.Publisher = dto.Publisher;
        book.PublishedYear = dto.PublishedYear;
        book.CategoryId = dto.CategoryId;
        book.AuthorId = dto.AuthorId;
        book.TotalCopies = dto.TotalCopies;
        book.IsActive = dto.IsActive;

        // Replace Avatar
        if (avatar != null)
        {
            string folder = Path.Combine(_env.WebRootPath, "book_images");
            Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, avatar.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await avatar.CopyToAsync(stream);

            book.Avatar = "/book_images/" + avatar.FileName;
        }

        // Replace PDF
        if (pdfFile != null)
        {
            string folder = Path.Combine(_env.WebRootPath, "book_pdf");
            Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, pdfFile.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await pdfFile.CopyToAsync(stream);

            book.Pdf = "/book_pdf/" + pdfFile.FileName;
        }

        await _context.SaveChangesAsync();
        return Ok(book);
    }

    // =======================================================================
    // SOFT DELETE
    // =======================================================================
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();

        book.IsDeleted = true;
        book.IsActive = false;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Book soft deleted" });
    }

    // =======================================================================
    // GET DELETED BOOKS
    // =======================================================================
    [HttpGet("deleted")]
    public async Task<IActionResult> GetDeletedBooks()
    {
        var deleted = await _context.Books
            .Where(b => b.IsDeleted)
            .Select(b => new
            {
                b.BookId,
                b.Title,
                b.Description,
                b.Avatar,
                b.Pdf,
                b.IsDeleted
            })
            .ToListAsync();

        return Ok(deleted);
    }

    // =======================================================================
    // RESTORE BOOK
    // =======================================================================
    [HttpPatch("{id}/restore")]
    public async Task<IActionResult> RestoreBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
            return NotFound("Book not found.");

        if (!book.IsDeleted)
            return BadRequest("Book is not deleted.");

        book.IsDeleted = false;
        book.IsActive = true;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Book restored successfully" });
    }

    // =======================================================================
    // HARD DELETE (Delete DB record + Avatar + PDF)
    // =======================================================================
    [HttpDelete("{id}/hard")]
    public async Task<IActionResult> HardDeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
            return NotFound("Book not found.");

        // Delete Avatar
        if (!string.IsNullOrEmpty(book.Avatar))
        {
            string path = Path.Combine("wwwroot", book.Avatar.TrimStart('/'));
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

        // Delete PDF
        if (!string.IsNullOrEmpty(book.Pdf))
        {
            string path = Path.Combine("wwwroot", book.Pdf.TrimStart('/'));
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Book permanently deleted" });
    }

    // =======================================================================
    // READ PDF (Exercise 9)
    // =======================================================================
    [HttpGet("{id}/read")]
    public async Task<IActionResult> ReadPdf(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
            return NotFound("Book not found.");

        if (string.IsNullOrEmpty(book.Pdf))
            return NotFound("This book does not have a PDF file.");

        var fullUrl = $"{Request.Scheme}://{Request.Host}{book.Pdf}";

        return Ok(new
        {
            pdfUrl = fullUrl,
            message = "PDF available."
        });
    }
}
