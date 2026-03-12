using the_alkanz.Website.Data;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Repositories;

public interface ICategoryRepository
{
    public Task<Category> AddAsync(Category category);
    public Task<IEnumerable<Category>> GetAllAsync();
    public Task<bool> DeleteAsync(Guid id);


}
