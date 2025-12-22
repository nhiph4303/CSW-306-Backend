using System.ComponentModel.DataAnnotations;

namespace BookManagementSystem.DTO
{
    public class CreateOrderDto
    {
        [Required]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }

    public class CreateOrderItemDto
    {
        [Required]
        public int BookId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be > 0")]
        public int Quantity { get; set; }
    }
}
