public class AIRecommendationResult
{
    public string AIResponse { get; set; } = null!;
    public List<CareProductGetDto> RecommendedProducts { get; set; } = new();
}
