using LibraryManagementSytem.Models;
using System.ComponentModel.DataAnnotations;

public class Book
{
    [Key]
    public int BookId { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }
    public string? BookCode { get; set; }
    public string? Publisher { get; set; }
    public DateTime? PublishedYear { get; set; }

    public int CategoryId { get; set; }
    public int AuthorId { get; set; }

    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }

    public DateTime CreatedDate { get; set; }
    public string? Avatar { get; set; }
    public string? Pdf { get; set; }

    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;

    // Navigation
    public Category Category { get; set; } = null!;
    public Author Author { get; set; } = null!;
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
