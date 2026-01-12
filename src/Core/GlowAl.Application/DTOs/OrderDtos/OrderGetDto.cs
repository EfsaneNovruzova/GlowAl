using GlowAl.Application.DTOs.OrderItemDtos;
using GlowAl.Domain.Enums.OrderEnum;

namespace GlowAl.Application.DTOs.OrderDtos;

public class OrderGetDto
{
    public Guid Id { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public bool IsPaid { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItemGetDto> Products { get; set; } = new();
}
