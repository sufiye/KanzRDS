using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;
    public interface IAttachmentService
    {
        Task<AttechmentResponseDto> UploadAsync(Guid productId, IFormFile file);
        Task<List<AttechmentResponseDto>> GetByProductIdAsync(Guid productId);
        Task DeleteAsync(Guid id);
    }


