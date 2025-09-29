using GlowAl.Application.DTOs.ReviewDtos;
using GlowAl.Application.Shared.Responses;

namespace GlowAl.Application.Abstracts.Services;

public interface IReviewService
{
    Task<BaseResponse<ReviewGetDto>> CreateAsync(Guid productId, string userId, ReviewCreateDto dto);
    Task<BaseResponse<List<ReviewGetDto>>> GetByProductIdAsync(Guid productId);
    Task<BaseResponse<ReviewGetDto>> UpdateAsync(Guid reviewId, string userId, ReviewUpdateDto dto);
    Task<BaseResponse<bool>> DeleteAsync(Guid reviewId, string userId, bool isAdmin);
}
