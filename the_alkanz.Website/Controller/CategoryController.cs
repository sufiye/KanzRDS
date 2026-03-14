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

    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> Creat(CreateCategoryRequestDto createCategoryRequestDto)
    {
        var category = await _categoryService.CreatAsync(createCategoryRequestDto);

        if (category == null) 
                            return BadRequest();

        return Ok(category);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
       var deleteCategory = await _categoryService.DeleteAsync(id);

        if(deleteCategory is false) 
                                return NotFound();

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        if (categories is null)
                       return NotFound();

        return Ok(categories);
    }
}
