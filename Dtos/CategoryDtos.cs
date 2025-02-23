namespace BlogManagementSystem.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }

    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }

    public class UpdateCategoryDto
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }
}
