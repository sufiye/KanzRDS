namespace the_alkanz.Website.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid  CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public decimal  Price { get; set; }
    public int StockCount { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; } = null!;

    public ICollection<ProductAttachment> Attachments { get; set; } = new List<ProductAttachment>();

}
