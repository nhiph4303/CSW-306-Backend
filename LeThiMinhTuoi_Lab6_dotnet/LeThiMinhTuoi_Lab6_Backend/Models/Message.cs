using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeThiMinhTuoi_Lab6_Backend.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [MaxLength(50)]
        public string? RoomName { get; set; }

        public DateTime CreatedAt { get; set; }

        public byte MessageType { get; set; } 
        // 0: broadcast, 1: group, 2: private

        public string? SenderId { get; set; }
        public virtual AppUser? Sender { get; set; }
        public string? ReceiverId { get; set; }
        public virtual AppUser? Receiver { get; set; }
    }
}
