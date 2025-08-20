namespace GlowAl.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public Category? Parent { get; set; }

    // Alt kategoriyalar
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    public ICollection<CareProduct> Products { get; set; } = new List<CareProduct>();
}

