using GlowAl.Application.DTOs.CareProductDtos;
using GlowAl.Application.Shared.Responses;

namespace GlowAl.Application.Abstracts.Services;

public interface ICareProductService
{
    Task<CareProductGetDto> CreateAsync(CareProductCreateDto dto, string userId);
    Task<CareProductGetDto> UpdateAsync(Guid id, CareProductUpdateDto dto, string userId);
    Task DeleteAsync(Guid id, string userId);
    Task<CareProductGetDto> GetByIdAsync(Guid id);
    Task<PagedResult<CareProductGetDto>> GetAllAsync(CareProductFilter filter);
    Task<List<CareProductGetDto>> GetBySkinProblemsAsync(SkinProblemQueryDto dto);
    Task<AiRecommendationResponseDto> GetAiRecommendationAsync(AiRecommendationRequestDto request);
}






