using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace bookMS.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string FullName { get; set; } = null!;
        [Required, StringLength(255), EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public String Password { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!; //Admin, User
        public DateTime CreatedAt { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
