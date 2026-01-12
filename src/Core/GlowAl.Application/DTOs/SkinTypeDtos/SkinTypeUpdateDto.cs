namespace GlowAl.Application.DTOs.SkinTypeDtos;
public class SkinTypeUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}