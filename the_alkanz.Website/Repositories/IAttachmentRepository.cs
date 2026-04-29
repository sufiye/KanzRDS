using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Repositories;

public interface IAttachmentRepository
{
    Task AddAsync(ProductAttachment attachment);
    Task<ProductAttachment?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductAttachment>> GetByProductIdAsync(Guid productId);
    Task DeleteAsync(ProductAttachment attachment);
}