using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSytem.Models;

[Table("Loans")]
public class Loan
{
    [Key]
    public int LoanId { get; set; }

    public int UserId { get; set; }
    public int BookId { get; set; }

    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public int Status { get; set; }   // 0: Active, 1: Returned, 2: Overdue

    // Navigation
    public User User { get; set; } = null!;
    public Book Book { get; set; } = null!;
}
