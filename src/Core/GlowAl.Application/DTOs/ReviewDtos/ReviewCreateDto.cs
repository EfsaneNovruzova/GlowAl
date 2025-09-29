namespace GlowAl.Application.DTOs.ReviewDtos;

public class ReviewCreateDto
{
    public int Rating { get; set; } 
    public string Comment { get; set; } = null!;
}
