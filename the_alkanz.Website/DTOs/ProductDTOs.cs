
namespace the_alkanz.Website.DTOs;

public class CreateProductRequestDto
{
    /// <summary>
    /// The name of the candle product.
    /// </summary>
    /// <example>Lavender Candle</example>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Short title of the candle.
    /// </summary>
    /// <example>Handmade Lavender Scented Candle</example>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the candle product.
    /// </summary>
    /// <example>A handmade soy wax candle with a relaxing lavender fragrance.</example>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Identifier of the category to which the candle belongs.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Price of the candle.
    /// </summary>
    /// <example>12.50</example>
    public decimal Price { get; set; }

    /// <summary>
    /// Available stock quantity.
    /// </summary>
    /// <example>30</example>
    public int StockCount { get; set; }
}


public class UpdateProductRequestDto
{
    /// <summary>
    /// Updated name of the candle.
    /// </summary>
    /// <example>Vanilla Candle</example>
    public string? Name { get; set; } = string.Empty;

    /// <summary>
    /// Updated title of the candle.
    /// </summary>
    /// <example>Handmade Vanilla Scented Candle</example>
    public string? Title { get; set; } = string.Empty;

    /// <summary>
    /// Updated description of the candle.
    /// </summary>
    /// <example>A natural soy wax candle with warm vanilla fragrance.</example>
    public string? Description { get; set; } = string.Empty;

    /// <summary>
    /// Updated category identifier.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Updated price of the candle.
    /// </summary>
    /// <example>14.99</example>
    public decimal? Price { get; set; }

    /// <summary>
    /// Updated stock quantity.
    /// </summary>
    /// <example>50</example>
    public int? StockCount { get; set; }
}

public class ProductResponseDto
{
    public Guid Id { get; set; }
    /// <summary>
    /// The name of the candle.
    /// </summary>
    /// <example>Lavender Candle</example>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Short title of the candle.
    /// </summary>
    /// <example>Handmade Lavender Scented Candle</example>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the candle.
    /// </summary>
    /// <example>A handmade soy wax candle with a relaxing lavender fragrance.</example>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Identifier of the category the candle belongs to.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Price of the candle.
    /// </summary>
    /// <example>12.50</example>
    public decimal Price { get; set; }

    /// <summary>
    /// Available stock quantity.
    /// </summary>
    /// <example>30</example>
    public int StockCount { get; set; }

    public List<AttechmentResponseDto> Attachments { get; set; } = new List<AttechmentResponseDto>();
}

public class ProductQueryParams
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Sort { get; set; }
    public string? SortDirection { get; set; }
    public string? Search { get; set; }
    public string? SearchTittle { get; set; }
    public string? SearchDescription { get; set; }

    public void Validate()
    {
        if (Page < 1) Page = 1;
        if (PageSize < 1) PageSize = 1;
        if (PageSize > 100) PageSize = 100;
        if (string.IsNullOrEmpty(SortDirection)) SortDirection = "asc";

        SortDirection = SortDirection.ToLower();

        if (SortDirection != "asc" && SortDirection != "desc") SortDirection = "asc";
    }

}