using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookManagementSystem.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        // not null
        [Required(ErrorMessage = "FullName is required"), StringLength(200, ErrorMessage = "FullName cannot exceed 200 characters")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(255)]
        public string Email { get; set; } = null!;



        // not null
        // max length = max -> no need to add
        [Required]
        public String Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!; //Admin, User

        // Default GetDate() (similar to DateTime.Now)
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 1 user can have many orders
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
