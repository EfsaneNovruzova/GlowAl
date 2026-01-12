namespace GlowAl.Application.DTOs.OrderItemDtos;

public class OrderItemCreateDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
