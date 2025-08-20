namespace GlowAl.Domain.Entities;

public class CareProduct:BaseEntity
{
    public string Name { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Ingredients { get; set; } = null!;
    public decimal Price { get; set; } = 0;

    public Guid? SkinTypeId { get; set; }
    public SkinType? SkinType { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public ICollection<ProductProblem> ProductProblems { get; set; } = new List<ProductProblem>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Article> Articles { get; set; } = new List<Article>();
}
