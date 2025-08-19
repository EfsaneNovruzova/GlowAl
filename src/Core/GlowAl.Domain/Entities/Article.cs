namespace GlowAl.Domain.Entities;

public class Article: BaseEntity
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;

    public Guid? SkinProblemId { get; set; }
    public SkinProblem? SkinProblem { get; set; }

    public Guid? SkinTypeId { get; set; }
    public SkinType? SkinType { get; set; }

    public Guid? ProductId { get; set; }
    public CareProduct? Product { get; set; }
}
