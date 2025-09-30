using GlowAl.Application.DTOs.OrderDtos;
using GlowAl.Application.Shared.Responses;

public interface IOrderService
{
    Task<BaseResponse<OrderGetDto>> CreateOrderAsync(string userId, OrderCreateDto dto);
    Task<BaseResponse<OrderGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<List<OrderGetDto>>> GetMyOrdersAsync(string userId);
    Task<BaseResponse<string>> CancelOrderAsync(Guid id, string userId);
    Task<BaseResponse<string>> PayOrderAsync(Guid id, string userId);
}
