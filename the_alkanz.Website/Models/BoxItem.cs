namespace the_alkanz.Website.Models;

public class BoxItem
{
    public Guid Id { get; set; }
    public Guid BoxId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
