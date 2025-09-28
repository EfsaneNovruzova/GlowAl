namespace GlowAl.Application.DTOs.CategoryDtos;

public class CategoryCreateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
}
