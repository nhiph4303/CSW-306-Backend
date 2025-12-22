using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookManagementSystem.Models
{
    [Table("OrderItems")]
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        // foreign key
        // not null
        //public int OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; } = null!;

        // foreign key
        //public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; } = null!;

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        // default getdate()
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
