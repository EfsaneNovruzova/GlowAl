using GlowAl.Domain.Entities;
using GlowAl.Persistence.Contexts;
using GlowAl.Application.Abstracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GlowAl.Persistence.Repositories;

public class CareProductRepository : Repository<CareProduct>, ICareProductRepository
{
    private readonly GlowAlDbContext _context;

    public CareProductRepository(GlowAlDbContext context) : base(context)
    {
        _context = context;
    }

    // Filtered IQueryable
    public IQueryable<CareProduct> GetAllFiltered()
    {
        return _context.CareProducts
                       .Include(p => p.Category)
                       .Include(p => p.SkinType)
                       .Include(p => p.ProductProblems)
                           .ThenInclude(pp => pp.SkinProblem)
                       .Include(p => p.Reviews)
                       .AsQueryable();
    }
}
