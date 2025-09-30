using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.OrderDtos;
using GlowAl.Application.Shared;
using GlowAl.Application.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace GlowAl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost]
        [Authorize(Policy = Permissions.Order.Create)]
        [ProducesResponseType(typeof(BaseResponse<OrderGetDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseResponse<OrderGetDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<OrderGetDto>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        {
            var userId = GetUserId();
            var response = await _orderService.CreateOrderAsync(userId, dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("my")]
        [Authorize(Policy = Permissions.Order.GetMyOrders)]
        [ProducesResponseType(typeof(BaseResponse<List<OrderGetDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<OrderGetDto>>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetUserId();
            var response = await _orderService.GetMyOrdersAsync(userId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(BaseResponse<OrderGetDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<OrderGetDto>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _orderService.GetByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("{id}/cancel")]
        [Authorize(Policy = Permissions.Order.Cancel)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var userId = GetUserId();
            var response = await _orderService.CancelOrderAsync(id, userId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("{id}/pay")]
        [Authorize(Policy = Permissions.Order.Pay)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Pay(Guid id)
        {
            var userId = GetUserId();
            var response = await _orderService.PayOrderAsync(id, userId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}

