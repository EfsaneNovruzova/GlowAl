using GlowAl.Domain.Entities;

public interface IAIQueryHistoryService
{
    Task<AIQueryHistory> AddAsync(AIQueryHistory history);
    Task<List<AIQueryHistory>> GetByUserIdAsync(Guid userId);
}
