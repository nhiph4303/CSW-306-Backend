
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSytem.Data;
public class CarouselDto
{
    public class CarouselCreationDto
    {
        public string ImageUrl { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? LinkUrl { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;
    }
}