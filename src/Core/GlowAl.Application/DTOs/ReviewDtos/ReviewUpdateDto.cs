namespace GlowAl.Application.DTOs.ReviewDtos;

public class ReviewUpdateDto
{
    public int Rating { get; set; } 
    public string Comment { get; set; } = null!;
}