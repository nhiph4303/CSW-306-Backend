namespace LibraryManagementSytem.Data;

public class LoanCreateDto
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime DueDate { get; set; }
}

public class LoanUpdateDto
{
    public DateTime? ReturnDate { get; set; }
}
