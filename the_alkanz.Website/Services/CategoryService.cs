using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services
{
    public class CategoryService : ICategoryService
    {
        public Task<CategoryResponseDto> CreatAsync(CreateCategoryRequestDto createCategoryRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
