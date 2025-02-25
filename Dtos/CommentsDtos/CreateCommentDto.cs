using BlogManagementSystem.Models;

namespace BlogManagementSystem.Dtos.CommentsDtos
{
    public class CreateCommentDto
    {
        public required string CommentContent { get; set; }
        public required DateTime CommentCreatedAt { get; set; }

        public int UserId { get; set; }//fk to user

        public int BlogId { get; set; }//fk to blogs

    }
}
