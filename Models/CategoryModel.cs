using System.ComponentModel.DataAnnotations;

namespace BlogManagementSystem.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; } 

        public required string CategoryName { get; set; }

        public string? CategoryDescription { get; set; }

        // One-to-Many with Blog
        public ICollection<BlogModel> BlogModel { get; set; } = new List<BlogModel>();
    }
}
