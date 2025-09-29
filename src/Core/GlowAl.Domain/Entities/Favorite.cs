namespace GlowAl.Domain.Entities;
    public class Favorite : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public AppUser User { get; set; } = null!;

        public Guid ProductId { get; set; }
        public CareProduct Product { get; set; } = null!;
    }


