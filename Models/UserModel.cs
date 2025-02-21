using System.ComponentModel.DataAnnotations;

namespace BlogManagementSystem.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        public required string UserName { get; set; }

        public required string UserEmail { get; set; }

        public int UserPhoneNumber { get; set; }

        public byte[]? UserDp { get; set; }

        public bool UserIsAuthor { get; set; }

        public DateTime UserCreatedAt { get; set; }


    }
}
