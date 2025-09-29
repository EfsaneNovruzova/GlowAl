using GlowAl.Application.Abstracts.Repositories;
using GlowAl.Domain.Entities;
using GlowAl.Persistence.Contexts;

namespace GlowAl.Persistence.Repositories;

public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
{
    public FavoriteRepository(GlowAlDbContext context) : base(context)
    {
    }
}
