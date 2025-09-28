using GlowAl.Domain.Entities;
using GlowAl.Persistence.Repositories;
using static GlowAl.Application.Shared.Permissions;
using CareProduct = GlowAl.Domain.Entities.CareProduct;

namespace GlowAl.Application.Abstracts.Repositories;

public interface ICareProductRepository : IRepository<CareProduct>
{
}
