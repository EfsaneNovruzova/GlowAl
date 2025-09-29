using GlowAl.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GlowAl.Persistence.Contexts;

public class GlowAlDbContext : IdentityDbContext<AppUser>
{
    public GlowAlDbContext( DbContextOptions<GlowAlDbContext> options):base(options)
    {  
    }
    

    public DbSet<Article>  Articles { get; set; }
    public DbSet<AIQueryHistory> AIQueryHistories { get; set; }
    public DbSet<CareProduct> CareProducts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductProblem> ProductProblems { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<SkinProblem> SkinProblems { get; set; }
    public DbSet<SkinType> SkinTypes { get; set; }
    public DbSet<Favorite> Favorite { get; set; }
}
