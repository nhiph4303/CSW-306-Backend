using System.ComponentModel.DataAnnotations;

namespace LeThiMinhTuoi_Lab6_Backend.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
