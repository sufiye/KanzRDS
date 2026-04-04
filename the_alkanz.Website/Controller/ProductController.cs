using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using the_alkanz.Website.common;
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

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="createProductRequest">The data required to create a new product.</param>
    /// <returns>The created product information.</returns>
    /// <response code="200">Product successfully created.</response>
    /// <response code="400">Invalid product data.</response>
    /// <response code="401">Unauthorized request.</response>
    /// <response code="403">User does not have permission to create products.</response>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductResponseDto>> Creat(CreateProductRequestDto createProductRequest)
    {
        var product = await _productService.CreatAsync(createProductRequest);

        if (product == null)
            return BadRequest();

        return Ok(product);
    }

    /// <summary>
    /// Deletes a product by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <returns>No content if the product was successfully deleted.</returns>
    /// <response code="204">Product successfully deleted.</response>
    /// <response code="404">Product not found.</response>
    /// <response code="401">Unauthorized request.</response>
    /// <response code="403">User does not have permission to delete products.</response>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        var deleteProduct = await _productService.DeleteAsync(id);

        if (deleteProduct is false)
            return NotFound("Product not found !");

        return NoContent();
    }

    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <returns>A list of all products.</returns>
    /// <response code="200">Returns the list of products.</response>
    /// <response code="404">No products found.</response>
    /// <response code="401">Unauthorized request.</response>
    [HttpGet]
   
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
    {
        var products = await _productService.GetAllAsync();

        if (products is null)
            return NotFound();

        return Ok(products);
    }

    /// <summary>
    /// Retrieves a product by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <returns>The product information.</returns>
    /// <response code="200">Returns the product.</response>
    /// <response code="404">Product not found.</response>
    /// <response code="401">Unauthorized request.</response>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<ProductResponseDto>> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product is null)
            return NotFound("Product not found !");

        return Ok(product);
    }
    /// <summary>
    /// Retrieves a paginated list of products based on the provided query parameters.
    /// </summary>
    /// <param name="productQuery">The query parameters for pagination, filtering, and sorting.</param>
    /// <returns>A paged result containing products and pagination info.</returns>
    /// <response code="200">Returns a paginated list of products.</response>
    /// <response code="400">Invalid query parameters.</response>
    /// <response code="401">Unauthorized request.</response>
    [HttpGet("pagedResult")]
    [ProducesResponseType(typeof(PageResult<ProductResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PageResult<ProductResponseDto>>> GetPaged([FromQuery] ProductQueryParams productQuery)
    {
        var products = await _productService.GetPagedAsync(productQuery);
        return Ok(products);
    }

    /// <summary>
    /// Retrieves all products that belong to a specific category.
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category.</param>
    /// <returns>A list of products in the specified category.</returns>
    /// <response code="200">Returns the list of products.</response>
    /// <response code="404">No products found for the specified category.</response>
    /// <response code="401">Unauthorized request.</response>
    [HttpGet("{categoryId:guid}/category")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProductsByCategoryId(Guid categoryId)
    {
        var products = await _productService.GetProductsByCategoryId(categoryId);

        if (products is null)
            return NotFound("Product not found !");

        return Ok(products);
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <param name="createProductRequest">The updated product data.</param>
    /// <returns>The updated product information.</returns>
    /// <response code="200">Product successfully updated.</response>
    /// <response code="404">Product not found.</response>
    /// <response code="401">Unauthorized request.</response>
    /// <response code="403">User does not have permission to update products.</response>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductResponseDto>> Update(Guid id, UpdateProductRequestDto createProductRequest)
    {
        var productUpdate = await _productService.UpdateAsync(id, createProductRequest);

        if (productUpdate is null)
            return NotFound("Product not found !");

        return Ok(productUpdate);
    }
}
