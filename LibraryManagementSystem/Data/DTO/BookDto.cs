namespace LibraryManagementSytem.Data;

public class BookUpdateDto
{
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? BookCode { get; set; }
    public string? Publisher { get; set; }
    public DateTime? PublishedYear { get; set; }

    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;

    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;

    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }

    public string? Avatar { get; set; }
    public string? Pdf { get; set; }

    public bool IsActive { get; set; }
}
