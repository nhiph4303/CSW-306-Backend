using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookManagementSystem.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(150, ErrorMessage = "Author cannot exceed 150 characters")]
        public string Author { get; set; } = null!;

        [Required(ErrorMessage = "Price is required")]
        // price must be positive
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        // decimal(10,2)
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        // foreign key
        public Genre Genre { get; set; } = null!;

        // Default = 0
        public int StockQuantity { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 1 book can have many orderItems
        [JsonIgnore]
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
