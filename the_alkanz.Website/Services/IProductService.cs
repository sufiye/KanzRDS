using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public interface IProductService
{
    public Task<ProductResponseDto> CreatAsync(CreateProductRequestDto createProductRequest);
    public Task<ProductResponseDto> UpdateAsync(Guid id,UpdateProductRequestDto createProductRequest);
    public Task<IEnumerable<ProductResponseDto>> GetAllAsync();
    public Task<ProductResponseDto> GetByIdAsync(Guid id);
    public Task<bool> DeleteAsync(Guid id);

}
