using Microsoft.EntityFrameworkCore;
using the_alkanz.Website.Data;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;
using the_alkanz.Website.Storage;

namespace the_alkanz.Website.Services;

public class AttachmentService : IAttachmentService
{
    public const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public static readonly string[] AllowedExtensions =
    {
        ".jpg", ".jpeg", ".png", ".pdf", ".txt", ".zip"
    };

    public static readonly string[] AllowedContentTypes =
    {
        "image/jpeg",
        "image/png",
        "application/pdf",
        "text/plain",
        "application/zip",
        "application/x-zip-compressed"
    };

    private readonly KanzDbContext _context;
    private readonly IFileStorage _storage;

    public AttachmentService(KanzDbContext context, IFileStorage storage)
    {
        _context = context;
        _storage = storage;
    }

    public async Task<AttechmentResponseDto?> UploadAsync(
        Guid productId,
        Stream fileStream,
        string originalFileName,
        string contentType,
        long length,
        CancellationToken cancellationToken = default)
    {
        if (length > MaxFileSizeBytes)
            throw new ArgumentException("File too large (max 5MB)");

        var ext = Path.GetExtension(originalFileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
            throw new ArgumentException("Invalid file extension");

        if (!AllowedContentTypes.Contains(contentType, StringComparer.OrdinalIgnoreCase))
            throw new ArgumentException("Invalid content type");

        var product = await _context.Products.FindAsync([productId], cancellationToken);

        if (product is null)
            return null;

        var folderKey = $"products/{productId}";

        var info = await _storage.UploadAsync(
            fileStream,
            originalFileName,
            contentType,
            folderKey,
            cancellationToken
        );

        var attachment = new ProductAttachment
        {
            ProductId = productId,
            OriginalFileName = originalFileName,
            StoredFileName = info.StoredFileName,
            ContentType = contentType,
            Size = info.Size,
            UploadedAt = DateTimeOffset.UtcNow
        };

        _context.ProductAttachments.Add(attachment);
        await _context.SaveChangesAsync(cancellationToken);

        return new AttechmentResponseDto
        {
            Id = attachment.Id,
            ProductId = attachment.ProductId,
            OriginalFileName = attachment.OriginalFileName,
            ContentType = attachment.ContentType,
            Size = attachment.Size,
            UploadedAt = attachment.UploadedAt
        };
    }

    public async Task<(Stream stream, string fileName, string contentType)?> GetDownloadAsync(
        Guid attachmentId,
        CancellationToken cancellationToken = default)
    {
        var att = await _context.ProductAttachments
            .FirstOrDefaultAsync(a => a.Id == attachmentId, cancellationToken);

        if (att is null)
            return null;

        var key = $"products/{att.ProductId}/{att.StoredFileName}";

        var stream = await _storage.OpenAsync(key, cancellationToken);

        return (stream, att.OriginalFileName, att.ContentType);
    }

    public async Task<TaskAttachmentInfo?> GetAttachmentInfoAsync(
        Guid attachmentId,
        CancellationToken cancellationToken = default)
    {
        var att = await _context.ProductAttachments
            .FirstOrDefaultAsync(a => a.Id == attachmentId, cancellationToken);

        if (att is null)
            return null;

        return new TaskAttachmentInfo
        {
            Id = att.Id,
            productId = att.ProductId,
            StoredFileName = att.StoredFileName,
            StorageKey = $"products/{att.ProductId}/{att.StoredFileName}"
        };
    }

    public async Task<bool> DeleteAsync(
        Guid attachmentId,
        CancellationToken cancellationToken = default)
    {
        var att = await _context.ProductAttachments
            .FirstOrDefaultAsync(a => a.Id == attachmentId, cancellationToken);

        if (att is null)
            return false;

        var key = $"products/{att.ProductId}/{att.StoredFileName}";

        _context.ProductAttachments.Remove(att);

        await _context.SaveChangesAsync(cancellationToken);
        await _storage.DeleteAsync(key, cancellationToken);

        return true;
    }
}