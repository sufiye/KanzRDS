namespace the_alkanz.Website.Models;

public class BoxItem
{
    public Guid Id { get; set; }
    public Guid BoxId { get; set; }
    public Box Box { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
