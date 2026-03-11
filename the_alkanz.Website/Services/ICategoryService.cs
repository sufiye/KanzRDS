using the_alkanz.Website.DTOs;
namespace the_alkanz.Website.Services;

public interface ICategoryService
{
    public Task<CategoryResponseDto> CreatAsync(CreateCategoryRequestDto createCategoryRequestDto);
    public Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    public Task<bool> DeleteAsync(Guid id);
}
