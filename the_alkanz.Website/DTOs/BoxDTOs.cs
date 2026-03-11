using the_alkanz.Website.Models;

namespace the_alkanz.Website.DTOs;

public class CreateBoxItemRequestDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
public class CreatBoxRequestDto
{
    public string BoxName { get; set; } = string.Empty;

    public IEnumerable<CreateBoxItemRequestDto> BoxItems { get; set; } = new List<CreateBoxItemRequestDto>();
}

public class BoxItemResponseDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}

public class BoxResponseDto
{
    public Guid UserId { get; set; }
    public string BoxName { get; set; } = string.Empty; 
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public IEnumerable<BoxItemResponseDto> BoxItems { get; set; } = new List<BoxItemResponseDto>();
    public DateTimeOffset CreatedAt { get; set; }
}