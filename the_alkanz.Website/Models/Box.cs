namespace the_alkanz.Website.Models;

public class Box
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public IEnumerable<BoxItem> BoxItems { get; set; } = new List<BoxItem>();
}
