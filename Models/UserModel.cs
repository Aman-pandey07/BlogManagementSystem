using System.ComponentModel.DataAnnotations;

namespace BlogManagementSystem.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        public required string UserName { get; set; }

        public required string UserEmail { get; set; }

        public long? UserPhoneNumber { get; set; }

        public byte[]? UserDp { get; set; }

        public bool UserIsAuthor { get; set; }

        public DateTime UserCreatedAt { get; set; }

        public ICollection<BlogModel>? BlogModels { get; set; } = new List<BlogModel>();  //// One-to-Many with Blog

        public ICollection<CommentModel>? CommentModels { get; set; } = new List<CommentModel>(); //// One-to-Many with comments


    }
}
