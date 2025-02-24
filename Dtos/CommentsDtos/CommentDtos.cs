namespace BlogManagementSystem.Dtos.CommentsDtos
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentCreatedAt { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }
    }

    public class CreateCommentDto
    {
        public string CommentContent { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }
    }

    public class UpdateCommentDto
    {
        public string CommentContent { get; set; }
    }
}
