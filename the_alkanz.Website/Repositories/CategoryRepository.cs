using the_alkanz.Website.Data;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly KanzDbContext _context;

    public CategoryRepository(KanzDbContext context)
    {
        _context = context;
    }

    public Task<Category> AddAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
