using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;
    public interface IAttachmentService
    {
        Task<AttechmentResponseDto> UploadAsync(Guid productId, IFormFile file);
        Task<IEnumerable<AttechmentResponseDto>> GetByProductIdAsync(Guid productId);
        Task DeleteAsync(Guid id);
    }


