using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSytem.Models;

[Table("Users")]
public class User
{
    [Key]
    public int UserId { get; set; }

    [Required, StringLength(200)]
    public string Fullname { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public string Password { get; set; } = null!;

    [Required, EmailAddress, StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? UserCode { get; set; }

    public bool IsLocked { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }

    public string? ActiveCode { get; set; }
    public string? Avatar { get; set; }

    // Navigation
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
