using GlowAl.Domain.Entities;
using GlowAl.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

public class AIQueryHistoryService : IAIQueryHistoryService
{
    private readonly GlowAlDbContext _context;

    public AIQueryHistoryService(GlowAlDbContext context)
    {
        _context = context;
    }

    public async Task<AIQueryHistory> AddAsync(AIQueryHistory history)
    {
        _context.AIQueryHistories.Add(history);
        await _context.SaveChangesAsync();
        return history;
    }

    public async Task<List<AIQueryHistory>> GetByUserIdAsync(Guid userId)
    {
        return await _context.AIQueryHistories
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.CreatedAt)
            .ToListAsync();
    }
}
