using the_alkanz.Website.Models;

public class ProductAttachment
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string? imgUrl { get; set; }
    public DateTimeOffset UploadedAt { get; set; }
}