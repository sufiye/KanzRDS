using the_alkanz.Website.common;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Repositories;

public interface IProductRepository
{
    public Task<Product> AddAsync(Product product);
    public Task UpdateAsync(Product product);
    public Task<IEnumerable<Product>> GetAllAsync();
    public Task<Product>  GetByIdAsync(Guid id);
    Task<PageResult<Product>> GetPagedAsync(ProductQueryParams productQueryParams);
    public Task<IEnumerable<Product>>  GetProductsByCategoryId(Guid categoryId);
    public Task<bool>  DeleteAsync(Guid Id);
}
