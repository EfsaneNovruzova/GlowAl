namespace GlowAl.Domain.Entities;

public class CareProduct : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Ingredients { get; set; } = null!;
    public decimal Price { get; set; }

    public int Stock { get; set; } = 0;
    public string ImageUrl { get; set; } = null!;
    public double Rating { get; set; } = 0;
    public int SalesCount { get; set; } = 0; 
    public string CreatedByUserId { get; set; } = null!; 
    public Guid? SkinTypeId { get; set; }
    public SkinType? SkinType { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public ICollection<ProductProblem> ProductProblems { get; set; } = new List<ProductProblem>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Article> Articles { get; set; } = new List<Article>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
