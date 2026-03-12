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
    public async Task<ActionResult<ProductResponseDto>> Creat(CreateProductRequestDto createProductRequest)
    {
        var product = await _productService.CreatAsync(createProductRequest);

        if (product == null)
                return BadRequest();

        return Ok(product);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        var deleteProduct = await _productService.DeleteAsync(id);

        if (deleteProduct is false)
                    return NotFound("Product not found !");

        return NoContent();
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
    {
        var products = await _productService.GetAllAsync();

        if(products is null)  
                    return NotFound();

        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product is null) 
                    return NotFound("Product not found !");

        return Ok(product);
    }
    [HttpGet("{categoryId}/category")]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProductsByCategoryId(Guid categoryId)
    {
        var products = await _productService.GetProductsByCategoryId(categoryId);

        if (products is null) 
                    return NotFound("Product not found !");

        return Ok(products);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductResponseDto>> Update(Guid id, UpdateProductRequestDto createProductRequest)
    {
        var productUpdate = await _productService.UpdateAsync(id, createProductRequest);

        if (productUpdate is null) 
                    return NotFound("Product not found !");

        return Ok(productUpdate);
    }
}
