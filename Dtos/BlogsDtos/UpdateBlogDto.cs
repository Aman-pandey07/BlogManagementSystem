namespace BlogManagementSystem.Dtos.BlogsDtos
{
    public class UpdateBlogDto
    {
        public required string BlogTitle { get; set; }
        public required string BlogContent { get; set; }
        public string? BlogImage { get; set; }
        public int UserId { get; set; }  //FK to User table
        public int CategoryId { get; set; } //FK to category

    }
}
