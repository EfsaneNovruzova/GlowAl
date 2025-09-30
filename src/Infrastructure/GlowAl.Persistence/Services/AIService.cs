using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GlowAl.Application.AI
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AIService(IConfiguration configuration, HttpClient httpClient)
        {
            _apiKey = configuration["DeepSeekSettings:ApiKey"];
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> SendMessageAsync(string prompt)
        {
            var requestBody = new
            {
                model = "deepseek-chat",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                stream = false
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.deepseek.com/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return $"Error {response.StatusCode}: {responseString}";

            using var doc = JsonDocument.Parse(responseString);

            if (!doc.RootElement.TryGetProperty("choices", out var choices) || choices.GetArrayLength() == 0)
                return "No response from DeepSeek model.";

            var choice = choices[0];
            if (choice.TryGetProperty("message", out var msg) &&
                msg.TryGetProperty("content", out var contentProp))
            {
                return contentProp.GetString()?.Trim() ?? "Empty response.";
            }

            return "Unexpected response format.";
        }
    }
}
