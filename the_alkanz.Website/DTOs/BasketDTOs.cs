namespace the_alkanz.Website.DTOs;

public class CreateBasketItemRequestDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
public class BasketResponseDto
{
    public ProductResponseDto Product { get; set; } = new ProductResponseDto();
    public int Quantity { get; set; }
}