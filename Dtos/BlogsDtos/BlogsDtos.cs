namespace BlogManagementSystem.Dtos.BlogsDtos
{
    public class BlogDto
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public byte[] BlogImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }

    public class CreateBlogDto
    {
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public byte[] BlogImage { get; set; }
        public int UserId { get; set; }
    }

    public class UpdateBlogDto
    {
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public byte[] BlogImage { get; set; }
    }
}