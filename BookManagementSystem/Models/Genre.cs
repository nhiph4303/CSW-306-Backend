using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookManagementSystem.Models
{
    [Table("Genres")]
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        // not null
        [Required(ErrorMessage = "Genre name is required")]
        // max length = 100
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        public string? Description { get; set; }

        // Default GetDate() (similar to DateTime.Now)
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 1 genre can have many books
        [JsonIgnore]
        public ICollection<Book> Books { get; set; } = new List<Book>();
       
    }
}
