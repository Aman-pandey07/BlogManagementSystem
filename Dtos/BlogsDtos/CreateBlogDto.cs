namespace BlogManagementSystem.Dtos.BlogsDtos
{
    public class CreateBlogDto
    {
        public required string BlogTitle { get; set; }
        public required string BlogContent { get; set; }
        public string? BlogImage { get; set; }
        public required DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; } //FK to category
    }
}
