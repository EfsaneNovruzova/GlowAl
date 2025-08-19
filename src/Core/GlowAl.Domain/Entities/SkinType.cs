namespace GlowAl.Domain.Entities;

public class SkinType: BaseEntity
{
    public string Name { get; set; } = null!; // Dry, Oily, Combination, Sensitive
    public string Description { get; set; } = null!;

    //public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
    public ICollection<CareProduct> CareProducts { get; set; } = new List<CareProduct>();
    public ICollection<Article> Articles { get; set; } = new List<Article>();
}
