namespace the_alkanz.Website.DTOs;

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
public class OrderResponseDto
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = string.Empty;

    public IEnumerable<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
}