using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GlowAl.Domain.Entities;
using Microsoft.Extensions.Configuration;



public class AIService : IAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly IAIQueryHistoryService _historyService;

    public AIService(IConfiguration configuration, HttpClient httpClient, IAIQueryHistoryService historyService)
    {
        _apiKey = configuration["DeepSeekSettings:ApiKey"];
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        _historyService = historyService;
    }

    public async Task<string> SendMessageAsync(string prompt, Guid? userId = null)
    {
        var requestBody = new
        {
            model = "deepseek-chat",
            messages = new[] { new { role = "user", content = prompt } },
            stream = false
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://api.deepseek.com/chat/completions", content);
        var responseString = await response.Content.ReadAsStringAsync();

        string result;
        if (!response.IsSuccessStatusCode)
            result = $"Error {response.StatusCode}: {responseString}";
        else
        {
            using var doc = JsonDocument.Parse(responseString);
            if (!doc.RootElement.TryGetProperty("choices", out var choices) || choices.GetArrayLength() == 0)
                result = "No response from DeepSeek model.";
            else
            {
                var choice = choices[0];
                if (choice.TryGetProperty("message", out var msg) &&
                    msg.TryGetProperty("content", out var contentProp))
                {
                    result = contentProp.GetString()?.Trim() ?? "Empty response.";
                }
                else result = "Unexpected response format.";
            }
        }

        // Tarixçəyə yaz
        if (userId.HasValue)
        {
            var history = new AIQueryHistory
            {
                UserId = userId.Value,
                Prompt = prompt,
                Response = result
            };
            await _historyService.AddAsync(history);
        }

        return result;
    }
}

