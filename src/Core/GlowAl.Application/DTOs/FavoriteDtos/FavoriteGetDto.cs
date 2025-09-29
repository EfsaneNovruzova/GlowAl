namespace GlowAl.Application.DTOs.FavoriteDtos;

public class FavoriteGetDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
}
