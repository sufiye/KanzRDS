using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Repositories;

namespace the_alkanz.Website.Services;

public class AttachmentService : IAttachmentService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _region;
    private readonly IAttachmentRepository _repository;
    private readonly IMapper _mapper;
    public AttachmentService(
        IConfiguration configuration,
        IAttachmentRepository repository,
        IMapper mapper)
    {
        _repository = repository;

        var awsSection = configuration.GetSection("AWS");
        var accessKey = awsSection["AccessKey"];
        var secretKey = awsSection["SecretKey"];
        _bucketName = awsSection["S3BucketName"]!;
        _region = awsSection["Region"]!;

        var credentials = new Amazon.Runtime.BasicAWSCredentials(
            accessKey,
            secretKey
        );

        var config = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_region)
        };

        _s3Client = new AmazonS3Client(credentials, config);
        _mapper = mapper;
    }


    public async Task<AttechmentResponseDto> UploadAsync(Guid productId, IFormFile? file)
    {
        if (file == null || file.Length == 0)
            throw new Exception("File is empty");

        var fileExtension = Path.GetExtension(file.FileName);
        var key = $"products/{Guid.NewGuid()}{fileExtension}";

        using var stream = file.OpenReadStream();

        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = stream,
            ContentType = file.ContentType
        };

        await _s3Client.PutObjectAsync(request);

        var fileUrl = $"https://{_bucketName}.s3.{_region}.amazonaws.com/{key}";

        var attachment = new ProductAttachment
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            imgUrl = fileUrl,
            UploadedAt = DateTimeOffset.UtcNow
        };

        await _repository.AddAsync(attachment);

        return new AttechmentResponseDto
        {
            Id = attachment.Id,
            ProductId = attachment.ProductId,
            imgUrl = attachment.imgUrl,
            UploadedAt = attachment.UploadedAt
        };
    }

    public async Task<IEnumerable<AttechmentResponseDto>> GetByProductIdAsync(Guid productId)
    {
        var attachments = await _repository.GetByProductIdAsync(productId);

        return _mapper.Map<IEnumerable<AttechmentResponseDto>>(attachments);
    }

    public async Task DeleteAsync(Guid id)
    {
        var attachment = await _repository.GetByIdAsync(id);

        if (attachment == null)
            throw new Exception("Attachment not found");

        var url = new Uri(attachment.imgUrl!);
        var key = url.AbsolutePath.TrimStart('/');

        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = key
        };

        await _s3Client.DeleteObjectAsync(request);

        await _repository.DeleteAsync(attachment);
    }
}