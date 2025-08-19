namespace GlowAl.Domain.Entities;

public class ProductProblem :BaseEntity
{
    public Guid ProductId { get; set; }
    public CareProduct Product { get; set; } = null!;

    public Guid ProblemId { get; set; }
    public SkinProblem Problem { get; set; } = null!;
}
