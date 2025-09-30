using GlowAl.Application.DTOs.OrderItemDtos;

namespace GlowAl.Application.DTOs.OrderDtos;

public class OrderCreateDto
{
    public List<OrderItemCreateDto> Items { get; set; } = new();
}
