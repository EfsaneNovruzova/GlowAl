using Microsoft.AspNetCore.Identity;

namespace GlowAl.Domain.Entities;

public class AppUser : IdentityUser
{
    public string FulName { get; set; } = null!;
    public DateTime ExpiryDate { get; set; }
    public string? RefreshToken { get; set; } 
    public Guid? SkinTypeId { get; set; }
    public SkinType? SkinType { get; set; }


    public ICollection<AIQueryHistory> AIQueryHistories { get; set; } = new List<AIQueryHistory>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
