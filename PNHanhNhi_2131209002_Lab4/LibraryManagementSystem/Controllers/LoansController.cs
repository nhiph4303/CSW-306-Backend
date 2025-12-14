using LibraryManagementSytem.Data;
using LibraryManagementSytem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSytem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public LoansController(LibraryDbContext context)
    {
        _context = context;
    }

    // GET /api/loans
    [HttpGet]
    public async Task<IActionResult> GetLoans(
        int? userId = null,
        int? status = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var query = _context.Loans
            .AsNoTracking()
            .Include(l => l.Book)
            .Include(l => l.User)
            .AsQueryable();

        if (userId != null)
            query = query.Where(l => l.UserId == userId);

        if (status != null)
            query = query.Where(l => l.Status == status);

        if (from != null)
            query = query.Where(l => l.LoanDate >= from);

        if (to != null)
            query = query.Where(l => l.LoanDate <= to);

        var loans = await query
            .Select(l => new
            {
                l.LoanId,
                l.UserId,
                UserName = l.User.Fullname,
                l.BookId,
                BookTitle = l.Book.Title,
                l.LoanDate,
                l.DueDate,
                l.ReturnDate,
                l.Status
            })
            .ToListAsync();

        return Ok(loans);
    }

    // GET /api/loans/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLoan(int id)
    {
        var loan = await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .Where(l => l.LoanId == id)
            .Select(l => new
            {
                l.LoanId,
                l.UserId,
                UserName = l.User.Fullname,
                l.BookId,
                BookTitle = l.Book.Title,
                l.LoanDate,
                l.DueDate,
                l.ReturnDate,
                l.Status
            })
            .FirstOrDefaultAsync();

        if (loan == null) return NotFound();
        return Ok(loan);
    }

    // POST /api/loans
    [HttpPost]
    public async Task<IActionResult> CreateLoan([FromBody] LoanCreateDto dto)
    {
        var book = await _context.Books.FindAsync(dto.BookId);
        if (book == null)
            return BadRequest("Book not found.");

        if (book.AvailableCopies <= 0)
            return BadRequest("Book is out of stock.");

        var loan = new Loan
        {
            UserId = dto.UserId,
            BookId = dto.BookId,
            LoanDate = DateTime.Now,
            DueDate = dto.DueDate,
            Status = 0 // Active
        };

        book.AvailableCopies--;

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            loan.LoanId,
            loan.UserId,
            loan.BookId,
            loan.LoanDate,
            loan.DueDate,
            loan.Status
        });
    }

    // PUT /api/loans/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLoan(int id, [FromBody] LoanUpdateDto dto)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan == null) return NotFound();

        if (dto.ReturnDate == null)
            return BadRequest("ReturnDate is required.");

        loan.ReturnDate = dto.ReturnDate;
        loan.Status = 1; // Returned

        var book = await _context.Books.FindAsync(loan.BookId);
        if (book != null)
            book.AvailableCopies++;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            loan.LoanId,
            loan.UserId,
            loan.BookId,
            loan.LoanDate,
            loan.DueDate,
            loan.ReturnDate,
            loan.Status
        });
    }
}
