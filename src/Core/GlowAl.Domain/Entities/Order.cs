using GlowAl.Domain.Enums.OrderEnum;

namespace GlowAl.Domain.Entities;

public class Order : BaseEntity
{
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsPaid { get; set; } = false;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}