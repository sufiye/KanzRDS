
namespace the_alkanz.Website.DTOs;

public class CreateProductRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public decimal Price { get; set; }
    public int StockCount { get; set; }
}
public class UpdateProductRequestDto
{
    public string? Name { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public Guid? CategoryId { get; set; }
    public decimal? Price { get; set; }
    public int? StockCount { get; set; }
}

public class ProductResponseDto
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public decimal Price { get; set; }
    public int StockCount { get; set; }
}