namespace BlogManagementSystem.Dtos.CategoryDtos
{
    public class GetAllCategoryDto
    {
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
    }

   
}
