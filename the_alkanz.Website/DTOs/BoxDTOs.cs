namespace the_alkanz.Website.DTOs;

public class CreateBoxRequestDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
public class BoxResponseDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}