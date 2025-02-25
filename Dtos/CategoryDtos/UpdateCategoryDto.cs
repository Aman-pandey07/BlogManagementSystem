namespace BlogManagementSystem.Dtos.CategoryDtos
{
    public class UpdateCategoryDto
    {
        public required string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
    }
}
