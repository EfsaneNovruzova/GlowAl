namespace GlowAl.Application.DTOs.CategoryDtos;

public class CategoryGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public int SubCategoriesCount { get; set; }
    public int ProductsCount { get; set; }
    public List<CategoryGetDto>? Children { get; set; }
}
