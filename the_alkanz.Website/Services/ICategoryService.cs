using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;
namespace the_alkanz.Website.Services;

public interface ICategoryService
{
    public Task<CategoryResponseDto> CreatAsync(CreateCategoryRequestDto createCategoryRequestDto);
    public Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    public  Task<CategoryResponseDto> GetByIdAsync(Guid id);
    public Task<bool> DeleteAsync(Guid id);
}
