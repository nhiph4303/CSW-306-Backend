
using BookManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagementSystem.DTO
{

    public class BookCreationDto
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = null!;

        [Required, StringLength(150)]
        public string Author { get; set; } = null!;

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "gerneId không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Giá trị phải là số nguyên dương")]
        public int GenreId { get; set; }

        public int StockQuantity { get; set; }
    }

    public class BookUpdationDto
    {
        [StringLength(200)]
        public string? Title { get; set; } 

        [StringLength(150)]
        public string? Author { get; set; } 

        public decimal? Price { get; set; }

    
        public int? GenreId { get; set; }

        public int? StockQuantity { get; set; }
    }
}
