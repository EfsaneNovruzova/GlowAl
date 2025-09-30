using GlowAl.Application.Abstracts.Repositories;
using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.OrderDtos;
using GlowAl.Application.DTOs.OrderItemDtos;
using GlowAl.Application.Shared.Responses;
using GlowAl.Domain.Entities;
using GlowAl.Domain.Enums.OrderEnum;
using GlowAl.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace GlowAl.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IRepository<CareProduct> _productRepo;
        private readonly IOrderItemRepository _orderItemRepo;

        public OrderService(
            IOrderRepository orderRepo,
            IRepository<CareProduct> productRepo,
            IOrderItemRepository orderItemRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _orderItemRepo = orderItemRepo;
        }

        public async Task<BaseResponse<OrderGetDto>> CreateOrderAsync(string userId, OrderCreateDto dto)
        {
            if (!dto.Items.Any())
                return new BaseResponse<OrderGetDto>("No items in order", HttpStatusCode.BadRequest);

            var order = new Order { UserId = userId };

            decimal total = 0;
            foreach (var itemDto in dto.Items)
            {
                var product = await _productRepo.GetByIdAsync(itemDto.ProductId);
                if (product == null)
                    return new BaseResponse<OrderGetDto>($"Product {itemDto.ProductId} not found", HttpStatusCode.NotFound);

                if (product.Stock < itemDto.Quantity)
                    return new BaseResponse<OrderGetDto>($"Product {product.Name} is out of stock or not enough quantity", HttpStatusCode.BadRequest);

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price
                };

                order.OrderItems.Add(orderItem);
                total += product.Price * itemDto.Quantity;

                product.Stock -= itemDto.Quantity;
                _productRepo.Update(product);
            }

            order.TotalAmount = total;
            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveChangeAsync();

            // Reload order with Product included
            var createdOrder = await _orderRepo.GetByFiltered(o => o.Id == order.Id)
                                               .Include(o => o.OrderItems)
                                               .ThenInclude(oi => oi.Product)
                                               .FirstOrDefaultAsync();

            var result = MapToDto(createdOrder);
            return new BaseResponse<OrderGetDto>("Order created successfully", result, HttpStatusCode.Created);
        }

        public async Task<BaseResponse<OrderGetDto>> GetByIdAsync(Guid id)
        {
            var order = await _orderRepo.GetByFiltered(o => o.Id == id)
                                        .Include(o => o.OrderItems)
                                        .ThenInclude(oi => oi.Product)
                                        .FirstOrDefaultAsync();

            if (order == null)
                return new BaseResponse<OrderGetDto>("Order not found", HttpStatusCode.NotFound);

            var result = MapToDto(order);
            return new BaseResponse<OrderGetDto>("Success", result, HttpStatusCode.OK);
        }

        public async Task<BaseResponse<List<OrderGetDto>>> GetMyOrdersAsync(string userId)
        {
            var orders = await _orderRepo.GetByFiltered(o => o.UserId == userId)
                                         .Include(o => o.OrderItems)
                                         .ThenInclude(oi => oi.Product)
                                         .ToListAsync();

            var result = orders.Select(MapToDto).ToList();
            return new BaseResponse<List<OrderGetDto>>("Success", result, HttpStatusCode.OK);
        }

        public async Task<BaseResponse<string>> CancelOrderAsync(Guid id, string userId)
        {
            var order = await _orderRepo.GetByFiltered(o => o.Id == id && o.UserId == userId)
                                        .Include(o => o.OrderItems)
                                        .ThenInclude(oi => oi.Product)
                                        .FirstOrDefaultAsync();

            if (order == null)
                return new BaseResponse<string>("Order not found or access denied", HttpStatusCode.NotFound);

            if (order.IsPaid)
                return new BaseResponse<string>("Cannot cancel paid order", HttpStatusCode.BadRequest);

            order.Status = OrderStatus.Cancelled;
            _orderRepo.Update(order);

            // Restore stock
            foreach (var item in order.OrderItems)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                if (product != null)
                {
                    product.Stock += item.Quantity;
                    _productRepo.Update(product);
                }
            }

            await _orderRepo.SaveChangeAsync();
            return new BaseResponse<string>("Order cancelled", HttpStatusCode.OK);
        }

        public async Task<BaseResponse<string>> PayOrderAsync(Guid id, string userId)
        {
            var order = await _orderRepo.GetByFiltered(o => o.Id == id && o.UserId == userId)
                                        .Include(o => o.OrderItems)
                                        .ThenInclude(oi => oi.Product)
                                        .FirstOrDefaultAsync();

            if (order == null)
                return new BaseResponse<string>("Order not found or access denied", HttpStatusCode.NotFound);

            if (order.IsPaid)
                return new BaseResponse<string>("Order is already paid", HttpStatusCode.BadRequest);

            order.IsPaid = true;
            order.Status = OrderStatus.Paid;
            _orderRepo.Update(order);
            await _orderRepo.SaveChangeAsync();

            return new BaseResponse<string>("Order paid successfully", HttpStatusCode.OK);
        }

        // Helper: Map Order -> OrderGetDto
        private OrderGetDto MapToDto(Order order)
        {
            return new OrderGetDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                IsPaid = order.IsPaid,
                CreatedAt = order.CreatedAt,
                Products = order.OrderItems.Select(oi => new OrderItemGetDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }
    }
}



