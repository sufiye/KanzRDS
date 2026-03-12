using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public interface IProductService
{
    public Task<ProductResponseDto> CreatAsync(CreateProductRequestDto createProductRequest);
    public Task<ProductResponseDto> UpdateAsync(Guid id,UpdateProductRequestDto  updateProductRequest);
    public Task<IEnumerable<ProductResponseDto>> GetAllAsync();
    public Task<ProductResponseDto> GetByIdAsync(Guid id);
    public Task<IEnumerable<ProductResponseDto>> GetProductsByCategoryId(Guid categoryId);
    public Task<bool> DeleteAsync(Guid id);

}
