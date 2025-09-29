using GlowAl.Application.Abstracts.Services;
using GlowAl.Application.DTOs.FavoriteDtos;
using GlowAl.Application.Shared.Responses;
using GlowAl.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace GlowAl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Favorite.Create)]
        [ProducesResponseType(typeof(BaseResponse<FavoriteGetDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Add([FromBody] FavoriteCreateDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new BaseResponse<string>("User not found", HttpStatusCode.Unauthorized));

            var response = await _favoriteService.AddFavoriteAsync(userId, dto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{productId}")]
        [Authorize(Policy = Permissions.Favorite.Delete)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Remove(Guid productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new BaseResponse<string>("User not found", HttpStatusCode.Unauthorized));

            var response = await _favoriteService.RemoveFavoriteAsync(userId, productId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("my")]
        [Authorize]
        [ProducesResponseType(typeof(BaseResponse<List<FavoriteGetDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetMyFavorites()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new BaseResponse<string>("User not found", HttpStatusCode.Unauthorized));

            var response = await _favoriteService.GetMyFavoritesAsync(userId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
