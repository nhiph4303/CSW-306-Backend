using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSytem.Models;

[Table("Carousel")]
public class Carousel
{
    [Key]
    public int CarouselId { get; set; }

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required, StringLength(200)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? LinkUrl { get; set; }

    public int Order { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
