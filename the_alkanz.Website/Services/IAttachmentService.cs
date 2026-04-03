using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public interface IAttachmentService
{
    Task<AttechmentResponseDto?> UploadAsync(
            Guid productId,
            Stream fileStream,
            string originalFileName,
            string contentType,
            long length,
            string userId,
            CancellationToken cancellationToken = default);


    Task<(Stream stream, string fileName, string contentType)?> GetDownloadAsync(
        Guid attachmentId, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid attachmentId, CancellationToken cancellationToken = default);

    Task<TaskAttachmentInfo?> GetAttachmentInfoAsync(Guid attachmentId, CancellationToken cancellationToken = default);
}

public class TaskAttachmentInfo
{
    public Guid Id { get; set; }
    public Guid productId { get; set; }
    public string StoredFileName { get; set; } = string.Empty;
    public string StorageKey { get; set; } = string.Empty;
    public string UploadedUserId { get; set; } = string.Empty;
}