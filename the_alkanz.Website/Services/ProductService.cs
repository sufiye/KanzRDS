using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public class ProductService : IProductService
{
    public Task<ProductResponseDto> CreatAsync(CreateProductRequestDto createProductRequest)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponseDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponseDto> UpdateAsync(Guid id, UpdateProductRequestDto createProductRequest)
    {
        throw new NotImplementedException();
    }
}
