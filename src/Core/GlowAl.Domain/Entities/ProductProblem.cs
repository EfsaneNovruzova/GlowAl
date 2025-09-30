namespace GlowAl.Domain.Entities;

public class ProductProblem :BaseEntity
{
    public Guid CareProductId { get; set; }
    public CareProduct CareProduct { get; set; } = null!;

    public Guid SkinProblemId { get; set; }
    public SkinProblem SkinProblem { get; set; } = null!;
}
