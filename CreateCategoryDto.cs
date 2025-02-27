public class CreateCategoryDto
{
    public required string CategoryName { get; set; }
    public string? CategoryDescription { get; set; }
    public List<int> BlogModelIds { get; set; } = new List<int>();
}
