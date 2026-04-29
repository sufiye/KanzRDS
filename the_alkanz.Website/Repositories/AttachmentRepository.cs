using AutoMapper;
using Microsoft.EntityFrameworkCore;
using the_alkanz.Website.Data;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Repositories;

public class AttachmentRepository : IAttachmentRepository
{
    private readonly KanzDbContext _context;
    private readonly IMapper _mapper;

    public AttachmentRepository(KanzDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddAsync(ProductAttachment attachment)
    {
        await _context.ProductAttachments.AddAsync(attachment);
        await _context.SaveChangesAsync();
    }

    public async Task<ProductAttachment?> GetByIdAsync(Guid id)
    {
        return await _context.ProductAttachments
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<ProductAttachment>> GetByProductIdAsync(Guid productId)
    {
        return await _context.ProductAttachments
            .Where(x => x.ProductId == productId)
            .ToListAsync();
    }

    public async Task DeleteAsync(ProductAttachment attachment)
    {
        _context.ProductAttachments.Remove(attachment);
        await _context.SaveChangesAsync();
    }
}