public interface ICareProductAIService
{
    Task<AiRecommendationResponseDto> GetRecommendationsAsync(string query, Guid? userId = null);
}
