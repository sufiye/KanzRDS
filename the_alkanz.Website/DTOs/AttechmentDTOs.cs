namespace the_alkanz.Website.DTOs;

public class AttechmentResponseDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; } 
    public string? Url { get; set; }
    public DateTimeOffset UploadedAt { get; set; }
}