namespace the_alkanz.Website.DTOs;

public class AttechmentResponseDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }

    public string OriginalFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long Size { get; set; }

    public DateTimeOffset UploadedAt { get; set; }
}
