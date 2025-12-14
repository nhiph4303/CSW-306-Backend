using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSytem.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public bool IsActive { get; set; }

    public string? Avatar { get; set; }

    // Navigation
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
