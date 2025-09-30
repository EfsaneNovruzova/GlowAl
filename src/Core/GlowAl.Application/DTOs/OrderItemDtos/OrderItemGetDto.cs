namespace GlowAl.Application.DTOs.OrderItemDtos;

public class OrderItemGetDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
