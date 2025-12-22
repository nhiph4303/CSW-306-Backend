using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bookMS.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Title { get; set; } = null!;
        [Required, StringLength(150)]
        public string Author { get; set; } = null!;
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
        public int StockQuantity { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public DateTime CreatedAt { get; set; }
    }
}