using GlowAl.Application.Abstracts.Services;

public class CareProductAIService : ICareProductAIService
{
    private readonly IAIService _aiService;
    private readonly ICareProductService _careProductService;

    public CareProductAIService(IAIService aiService, ICareProductService careProductService)
    {
        _aiService = aiService;
        _careProductService = careProductService;
    }

    public async Task<AIRecommendationResult> GetRecommendationsAsync(string userQuery, Guid? userId = null)
    {
        // 1️⃣ AI cavabını al
        var aiResponse = await _aiService.SendMessageAsync(userQuery, userId);

        // 2️⃣ AI cavabından dərman problemlərini çıxar (keyword matching)
        var problems = ExtractSkinProblemsFromAI(aiResponse);

        // 3️⃣ Uyğun məhsulları al
        var recommendedProducts = await _careProductService.GetBySkinProblemsAsync(new SkinProblemQueryDto
        {
            Problems = problems
        });

        return new AIRecommendationResult
        {
            AIResponse = aiResponse,
            RecommendedProducts = recommendedProducts
        };
    }

    private List<string> ExtractSkinProblemsFromAI(string aiText)
    {
        var problems = new List<string>();

        if (aiText.ToLower().Contains("yagli") || aiText.ToLower().Contains("yağlı"))
            problems.Add("Yağlı dəri");
        if (aiText.ToLower().Contains("quru"))
            problems.Add("Quru dəri");
        if (aiText.ToLower().Contains("akne"))
            problems.Add("Akne");

        return problems;
    }
}
