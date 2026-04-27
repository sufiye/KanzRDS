namespace the_alkanz.Website.Repositories;

public interface IAttachmentRepository
{
    Task AddAsync(ProductAttachment attachment);
    Task<ProductAttachment?> GetByIdAsync(Guid id);
    Task<List<ProductAttachment>> GetByProductIdAsync(Guid productId);
    Task DeleteAsync(ProductAttachment attachment);
}