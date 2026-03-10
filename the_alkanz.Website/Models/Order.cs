namespace the_alkanz.Website.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public int TotalPrice { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
    public OrderStatus Status { get; set; } 
    public IEnumerable<OrderItem> OrderItems { get; set; }  = new List<OrderItem>();
}

public enum OrderStatus
{
    Pending,
    Shipped,
    Delivered
}