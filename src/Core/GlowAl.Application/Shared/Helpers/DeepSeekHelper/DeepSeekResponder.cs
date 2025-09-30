using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DeepSeekHelper
{
    public static class DeepSeekResponder
    {
        private static readonly HttpClient client = new HttpClient();
        private static string apiKey;

        public static void Initialize(IConfiguration configuration)
        {
            apiKey = configuration["DeepSeekSettings:ApiKey"];
        }

        public static async Task<string> SendMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("DeepSeek API key not set.");

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "deepseek-chat",
                messages = new[]
                {
                    new { role = "user", content = message }
                },
                stream = false
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("https://api.deepseek.com/chat/completions", content);
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
            catch (Exception ex)
            {
                return $"Request failed: {ex.Message}";
            }
        }
    }
}
