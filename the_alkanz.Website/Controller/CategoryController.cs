using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

/// <summary>
/// Provides API endpoints for managing product categories.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryController"/> class.
    /// </summary>
    /// <param name="categoryService">Service responsible for category-related operations.</param>
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="request">The data required to create a category.</param>
    /// <remarks>
    /// Only administrators are allowed to create categories.
    /// </remarks>
    /// <returns>
    /// The created category if successful; otherwise, a BadRequest response.
    /// </returns>
    /// <response code="200">Category successfully created.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="401">Unauthorized request.</response>
    /// <response code="403">User does not have permission to create categories.</response>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CategoryResponseDto>> Create(CreateCategoryRequestDto request)
    {
        var category = await _categoryService.CreatAsync(request);

        if (category == null)
            return BadRequest("Category could not be created.");

        return Ok(category);
    }

    /// <summary>
    /// Deletes a category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the category.</param>
    /// <remarks>
    /// Only administrators are allowed to delete categories.
    /// </remarks>
    /// <returns>
    /// No content if deletion is successful; otherwise, NotFound if the category does not exist.
    /// </returns>
    /// <response code="204">Category successfully deleted.</response>
    /// <response code="404">Category not found.</response>
    /// <response code="401">Unauthorized request.</response>
    /// <response code="403">User does not have permission to delete categories.</response>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var isDeleted = await _categoryService.DeleteAsync(id);

        if (!isDeleted)
            return NotFound("Category not found.");

        return NoContent();
    }

    /// <summary>
    /// Retrieves all available categories.
    /// </summary>
    /// <returns>
    /// A list of all categories. Returns NotFound if no categories exist.
    /// </returns>
    /// <response code="200">Returns the list of categories.</response>
    /// <response code="404">No categories found.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        if (categories == null || !categories.Any())
            return NotFound("No categories found.");

        return Ok(categories);
    }

    /// <summary>
    /// Retrieves a category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the category.</param>
    /// <returns>
    /// The requested category if found; otherwise, NotFound.
    /// </returns>
    /// <response code="200">Returns the requested category.</response>
    /// <response code="404">Category not found.</response>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryResponseDto>> GetById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound("Category not found.");

        return Ok(category);
    }
}