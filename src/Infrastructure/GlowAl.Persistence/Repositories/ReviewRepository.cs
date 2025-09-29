using GlowAl.Application.Abstracts.Repositories;
using GlowAl.Domain.Entities;
using GlowAl.Persistence.Contexts;

namespace GlowAl.Persistence.Repositories;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(GlowAlDbContext context) : base(context)
    {
    }
}