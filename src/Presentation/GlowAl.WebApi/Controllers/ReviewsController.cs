using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.ReviewDtos;
using GlowAl.Application.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using GlowAl.Application.Shared;
using System.Security.Claims;

namespace GlowAl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("{productId}")]
        [Authorize(Policy = Permissions.Review.Create)]
        [ProducesResponseType(typeof(BaseResponse<ReviewGetDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Add(Guid productId, [FromBody] ReviewCreateDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new BaseResponse<string>("User not found", HttpStatusCode.Unauthorized));

            var response = await _reviewService.CreateAsync(productId, userId, dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Permissions.Review.Update)]
        [ProducesResponseType(typeof(BaseResponse<ReviewGetDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] ReviewUpdateDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new BaseResponse<string>("User not found", HttpStatusCode.Unauthorized));

            var response = await _reviewService.UpdateAsync(id, userId, dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Review.Delete)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new BaseResponse<string>("User not found", HttpStatusCode.Unauthorized));

            var isAdmin = User.IsInRole("Admin");
            var response = await _reviewService.DeleteAsync(id, userId, isAdmin);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("by-product/{productId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<List<ReviewGetDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByProduct(Guid productId)
        {
            var response = await _reviewService.GetByProductIdAsync(productId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}


