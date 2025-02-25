using BlogManagementSystem.Models;

namespace BlogManagementSystem.Dtos.CommentsDtos
{
    public class UpdateCommentDto
    {
        public required string CommentContent { get; set; }

        public int UserId { get; set; }//fk to user

        public int BlogId { get; set; }//fk to blogs
    }
}
