using GlowAl.Application.DTOs.FavoriteDtos;
using GlowAl.Application.Shared.Responses;

namespace GlowAl.Application.Abstracts.Services;

public interface IFavoriteService
{
    Task<BaseResponse<FavoriteGetDto>> AddFavoriteAsync(string userId, FavoriteCreateDto dto);
    Task<BaseResponse<bool>> RemoveFavoriteAsync(string userId, Guid productId);
    Task<BaseResponse<List<FavoriteGetDto>>> GetMyFavoritesAsync(string userId);
}
