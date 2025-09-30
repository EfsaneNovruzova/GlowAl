using System.Threading.Tasks;

namespace GlowAl.Application.AI
{
    public interface IAIService
    {
        Task<string> SendMessageAsync(string prompt);
    }
}

