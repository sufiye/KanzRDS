using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
   private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="createCategoryRequestDto">The data required to create a category.</param>
    /// <returns>The created category.</returns>
    /// <response code="200">Category successfully created.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="401">Unauthorized request.</response>
    /// <response code="403">User does not have permission to create categories.</response>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CategoryResponseDto>> Creat(CreateCategoryRequestDto createCategoryRequestDto)
    {
        var category = await _categoryService.CreatAsync(createCategoryRequestDto);

        if (category == null)
            return BadRequest();

        return Ok(category);
    }

    /// <summary>
    /// Deletes a category by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the category.</param>
    /// <returns>No content if the category was successfully deleted.</returns>
    /// <response code="204">Category successfully deleted.</response>
    /// <response code="404">Category not found.</response>
    /// <response code="401">Unauthorized request.</response>
    /// <response code="403">User does not have permission to delete categories.</response>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        var deleteCategory = await _categoryService.DeleteAsync(id);

        if (deleteCategory is false)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <returns>A list of all available categories.</returns>
    /// <response code="200">Returns the list of categories.</response>
    /// <response code="404">No categories found.</response>
    /// <response code="401">Unauthorized request.</response>
    /// <response code="403">User does not have permission to access categories.</response>
    [HttpGet]

    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        if (categories is null)
            return NotFound();

        return Ok(categories);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryResponseDto>> GetById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        return Ok(category);
    }
}
