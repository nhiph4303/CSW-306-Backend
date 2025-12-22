using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookManagementSystem.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        //public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; } = null!;

        // not null
        [Required]
        // Validating that orderdate is not in the future will be implemented in Controller
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        // max length = 20
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        // default getdate()
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 1 order can have many orderitems
        [JsonIgnore]
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
