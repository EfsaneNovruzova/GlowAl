namespace GlowAl.Domain.Entities;

public class AIQueryHistory :BaseEntity
{
    public Guid? UserId { get; set; }
    public AppUser? User { get; set; }
    public string ProductName { get; set; } = null!;
    public string Response { get; set; } = null!;

}
