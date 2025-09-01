namespace GlowAl.Domain.Entities;

public class Review :BaseEntity
{
    public Guid ProductId { get; set; }
    public CareProduct Product { get; set; } = null!;
    public string UserId { get; set; }
    public AppUser User { get; set; } = null!;
    public int Rating { get; set; } // 1-5
    public string Comment { get; set; } = null!;
}
