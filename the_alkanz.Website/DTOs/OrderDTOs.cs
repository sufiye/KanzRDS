using the_alkanz.Website.Models;

namespace the_alkanz.Website.DTOs;

/// <summary>
/// Represents a single item inside an order.
/// </summary>
public class OrderItemResponseDto
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The price of the product at the time the order was created.
    /// </summary>
    /// <example>19.99</example>
    public decimal Price { get; set; }

    /// <summary>
    /// The quantity of the product ordered.
    /// </summary>
    /// <example>2</example>
    public int Quantity { get; set; }
}

/// <summary>
/// Represents the response returned when retrieving order information.
/// </summary>
public class OrderResponseDto
{
    /// <summary>
    /// The unique identifier of the order.
    /// </summary>
    /// <example>8c8f3f79-5f0c-4e6b-9c7a-1f3c8c2f6c92</example>
    public Guid Id { get; set; }

    /// <summary>
    /// The identifier of the user who created the order.
    /// </summary>
    /// <example>915c221b-e7e6-42d0-8c07-849bc0701f08</example>
    public Guid UserId { get; set; }

    /// <summary>
    /// The total price of all items in the order.
    /// </summary>
    /// <example>59.97</example>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// The date and time when the order was created.
    /// </summary>
    /// <example>2026-03-27T14:30:00Z</example>
    public DateTimeOffset OrderDate { get; set; }

    /// <summary>
    /// The current status of the order (Pending, Shipped, Delivered).
    /// </summary>
    /// <example>Pending</example>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// The list of items included in the order.
    /// </summary>
    public IEnumerable<OrderItemResponseDto> Items { get; set; } = new List<OrderItemResponseDto>();
}

/// <summary>
/// Represents a request to change the status of an order.
/// </summary>
public class OrderStatusChange
{
    /// <summary>
    /// The new status of the order.
    /// </summary>
    /// <example>Shipped</example>
    public string Status { get; set; } = string.Empty ;
}