using GlowAl.Application.Abstracts.Repositories;
using GlowAl.Domain.Entities;
using GlowAl.Persistence.Contexts;

namespace GlowAl.Persistence.Repositories;

public class CareProductRepository : Repository<CareProduct>, ICareProductRepository
{
    public CareProductRepository(GlowAlDbContext context) : base(context)
    {
    }
}
