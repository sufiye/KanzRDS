using Microsoft.EntityFrameworkCore;
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

    public async Task<Category> AddAsync(Category category)
    {
        _context.Categories.Add(category);

        await _context.SaveChangesAsync();

        return category;
        
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
         var deleteCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (deleteCategory is null) 
                                return false;

        _context.Categories.Remove(deleteCategory!);

        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categories = await _context.Categories.ToListAsync();

        return categories;
    }
}
