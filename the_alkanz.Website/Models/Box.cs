namespace the_alkanz.Website.Models;

public class Box
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string BoxName { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public IEnumerable<BoxItem> BoxItems { get; set; } = new List<BoxItem>();
}
