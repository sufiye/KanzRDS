using Microsoft.AspNetCore.Mvc;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public Task<ProductResponseDto> Creat(CreateProductRequestDto createProductRequest)
    {
        throw new NotImplementedException();
    }
    [HttpDelete]
    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
    [HttpGet]
    public Task<IEnumerable<ProductResponseDto>> GetAll()
    {
        throw new NotImplementedException();
    }
    [HttpGet("{id}")]
    public Task<ProductResponseDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    [HttpPut]
    public Task<ProductResponseDto> Update(Guid id, UpdateProductRequestDto createProductRequest)
    {
        throw new NotImplementedException();
    }
}
