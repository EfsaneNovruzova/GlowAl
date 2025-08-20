using GlowAl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlowAl.Persistence.Contexts;

public class GlowAlDbContext : DbContext
{
    public GlowAlDbContext( DbContextOptions<GlowAlDbContext> options):base(options)
    {  
    }
    

    public DbSet<Article>  articles { get; set; }
}
