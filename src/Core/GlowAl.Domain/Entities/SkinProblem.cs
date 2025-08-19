namespace GlowAl.Domain.Entities;

public class SkinProblem: BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Severity { get; set; } = "Medium"; // Low, Medium, High

    public ICollection<ProductProblem> ProductProblems { get; set; } = new List<ProductProblem>();
    public ICollection<Article> Articles { get; set; } = new List<Article>();
}
