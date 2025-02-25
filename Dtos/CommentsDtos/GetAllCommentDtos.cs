namespace BlogManagementSystem.Dtos.CommentsDtos
{
    public class GetCommentDto
    {
        public int CommentId { get; set; }
        public required string CommentContent { get; set; }
        public DateTime CommentCreatedAt { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }
    }

    

    
}
