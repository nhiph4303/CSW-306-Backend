using LibraryManagementSytem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LibraryManagementSystem.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required, StringLength(50)]
        public string RoleName { get; set; } // e.g., “Admin”, “User”, "Librarian" ,....

        [StringLength(200)]
        public string Description { get; set; }

        public ICollection<User> Users { get; } = [];

    }
}