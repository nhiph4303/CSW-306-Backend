using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSytem.Models;

[Table("Authors")]
public class Author
{
    [Key]
    public int AuthorId { get; set; }

    [Required, StringLength(100)]
    public string FirstName { get; set; } = null!;

    [Required, StringLength(100)]
    public string LastName { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public string? Biography { get; set; }

    [StringLength(100)]
    public string? Nationality { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? Website { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }

    public string? Avatar { get; set; }
    public bool IsDeleted { get; set; } = false;

    // Navigation
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
