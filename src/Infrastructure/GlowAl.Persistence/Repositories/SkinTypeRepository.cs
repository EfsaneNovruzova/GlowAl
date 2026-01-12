using GlowAl.Application.Abstracts.Repositories;
using GlowAl.Domain.Entities;
using GlowAl.Persistence.Contexts;

namespace GlowAl.Persistence.Repositories;

public class SkinTypeRepository : Repository<SkinType>, ISkinTypeRepository
{
    public SkinTypeRepository(GlowAlDbContext context) : base(context)
    {
    }
}
