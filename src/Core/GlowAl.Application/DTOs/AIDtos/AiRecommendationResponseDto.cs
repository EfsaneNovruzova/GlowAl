public class AiRecommendationResponseDto
{
    public string AiResponse { get; set; } = null!;
    public List<CareProductGetDto> RecommendedProducts { get; set; } = new();
}