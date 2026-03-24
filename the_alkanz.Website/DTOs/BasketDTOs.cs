namespace the_alkanz.Website.DTOs;

/// <summary>
/// Represents the request model used to add a product to the user's basket.
/// </summary>
public class CreateBasketItemRequestDto
{
    /// <summary>
    /// The unique identifier of the product that will be added to the basket.
    /// </summary>
    /// <example>c56a4180-65aa-42ec-a945-5fd21dec0538</example>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product to add to the basket.
    /// Must be greater than zero and should not exceed the available stock.
    /// </summary>
    /// <example>2</example>
    public int Quantity { get; set; }
}

/// <summary>
/// Represents the response model returned when retrieving basket items.
/// </summary>
public class BasketResponseDto
{
    /// <summary>
    /// The product information included in the basket item.
    /// </summary>
    /// <example>
    /// {
    ///   "id": "c56a4180-65aa-42ec-a945-5fd21dec0538",
    ///   "name": "Chocolate Cake",
    ///   "price": 15.50
    /// }
    /// </example>
    public ProductResponseDto Product { get; set; } = new ProductResponseDto();

    /// <summary>
    /// The quantity of the product in the basket.
    /// </summary>
    /// <example>2</example>
    public int Quantity { get; set; }
}