namespace the_alkanz.Website.Storage;

public interface IFileStorage
{
    Task<StoredFileInfo> UploadAsync(
       Stream stream,
       string orginalFileName,
       string contentType,
       string folderKey,
       CancellationToken cancellation = default);

    Task<Stream> OpenAsync(
      string storageKey,
      CancellationToken cancellation = default);

    Task DeleteAsync(
      string storageKey,
      CancellationToken cancellation = default);

}
