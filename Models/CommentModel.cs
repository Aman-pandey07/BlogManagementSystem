using System.ComponentModel.DataAnnotations;

namespace BlogManagementSystem.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentId { get; set; }
        public required string CommentContent { get; set; }
        public required DateTime CommentCreatedAt { get; set; }

        public int UserId { get; set; }//fk to user
        public required UserModel UserModel { get; set; }//this is refrence navigation for user 

        public int BlogId { get; set; }//fk to blogs
        public required BlogModel BlogModel { get; set; }///this is refrence navigation for blogs
    }
}
