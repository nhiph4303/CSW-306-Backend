namespace LibraryManagementSytem.Data;

public class TopBorrowedBookDto
{
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public int BorrowCount { get; set; }
}
