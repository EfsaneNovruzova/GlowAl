public class CareProductAIService : ICareProductAIService
{
    private readonly CareProductService _careProductService;

    public CareProductAIService(CareProductService careProductService)
    {
        _careProductService = careProductService;
    }

    public async Task<AiRecommendationResponseDto> GetRecommendationsAsync(string query, Guid? userId = null)
    {
        var request = new AiRecommendationRequestDto { Query = query, UserId = userId };
        return await _careProductService.GetAiRecommendationAsync(request);
    }
}

