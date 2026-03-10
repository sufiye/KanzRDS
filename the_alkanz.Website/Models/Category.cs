namespace the_alkanz.Website.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable< Product> Products { get; set; } = new List<Product>();
}

