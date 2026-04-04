using AutoMapper;
using the_alkanz.Website.common;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;
using the_alkanz.Website.Repositories;

namespace the_alkanz.Website.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _repository;

    public ProductService(IMapper mapper, IProductRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ProductResponseDto> CreatAsync(CreateProductRequestDto createProductRequest)
    {
        var product = _mapper.Map<Product>(createProductRequest);

        product = await _repository.AddAsync(product);

        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();

        if (products is null) return null!;

        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null) return null!;

        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<PageResult<ProductResponseDto>> GetPagedAsync(ProductQueryParams productQueryParams)
    {
   
        var pagedProducts = await _repository.GetPagedAsync(productQueryParams); 

        
        var productDtos = _mapper.Map<IEnumerable<ProductResponseDto>>(pagedProducts.Items);

        return PageResult<ProductResponseDto>.Creat(
            productDtos,
            pagedProducts.Page,
            pagedProducts.PageSize,
            pagedProducts.TotalCount
        );
    }

    public async Task<IEnumerable<ProductResponseDto>> GetProductsByCategoryId(Guid categoryId)
    {
        var products = await _repository.GetProductsByCategoryId(categoryId);

        if (products is null) return null!;

        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto> UpdateAsync(Guid id, UpdateProductRequestDto  updateProductRequest)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            return null!;

        _mapper.Map(updateProductRequest, product);

        await _repository.UpdateAsync(product);

        return _mapper.Map<ProductResponseDto>(product);
    }
}
