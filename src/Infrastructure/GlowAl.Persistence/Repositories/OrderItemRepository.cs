using GlowAl.Application.Abstracts.Repositories;
using GlowAl.Domain.Entities;
using GlowAl.Persistence.Contexts;
using GlowAl.Persistence.Repositories;

public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(GlowAlDbContext context) : base(context)
    {
    }
}
