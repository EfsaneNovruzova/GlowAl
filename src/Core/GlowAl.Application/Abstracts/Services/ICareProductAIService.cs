public interface ICareProductAIService
{
    Task<AIRecommendationResult> GetRecommendationsAsync(string userQuery, Guid? userId = null);
}
