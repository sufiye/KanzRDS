namespace the_alkanz.Website.Models;

public class ProductAttachment
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public string OriginalFileName {  get; set; } = string.Empty;
    public string StoredFileName {  get; set; } = string.Empty;
    public string ContentType {  get; set; } = string.Empty;
    public long Size {  get; set; } 

    public string UploadedUserId {  get; set; } = string.Empty;
    public ApplicationUser UploadedUser {  get; set; } = null!;
    public DateTimeOffset UploadedAt{  get; set; } 
}
