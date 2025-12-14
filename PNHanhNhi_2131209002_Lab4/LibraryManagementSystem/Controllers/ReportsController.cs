using LibraryManagementSytem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSytem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public ReportsController(LibraryDbContext context)
    {
        _context = context;
    }

    // GET /api/reports/top-borrowed?fromDate=...&toDate=...&top=10
    [HttpGet("top-borrowed")]
    public async Task<IActionResult> GetTopBorrowedBooks(
        DateTime? fromDate,
        DateTime? toDate,
        int top = 10)
    {
        if (fromDate == null || toDate == null)
            return BadRequest("fromDate and toDate are required.");

        var data = await _context.Loans
            .Where(l => l.LoanDate >= fromDate && l.LoanDate <= toDate)
            .GroupBy(l => new { l.BookId, l.Book.Title })
            .Select(g => new TopBorrowedBookDto
            {
                BookId = g.Key.BookId,
                Title = g.Key.Title,
                BorrowCount = g.Count()
            })
            .OrderByDescending(x => x.BorrowCount)
            .Take(top)
            .ToListAsync();

        return Ok(data);
    }
}
