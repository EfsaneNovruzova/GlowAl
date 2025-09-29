namespace GlowAl.Application.DTOs.ReviewDtos;

public class ReviewGetDto
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
}