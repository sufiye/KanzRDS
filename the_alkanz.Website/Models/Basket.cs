namespace the_alkanz.Website.Models;

public class Basket
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public IEnumerable<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
}
