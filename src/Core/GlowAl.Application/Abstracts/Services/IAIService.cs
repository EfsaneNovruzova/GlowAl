public interface IAIService
{
    Task<string> SendMessageAsync(string prompt, Guid? userId = null);
}

