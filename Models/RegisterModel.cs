using System.ComponentModel.DataAnnotations;

namespace BlogManagementSystem.Models
{
    public class RegisterModel
    {
        public required string Name { get; set; }
        [Key]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
