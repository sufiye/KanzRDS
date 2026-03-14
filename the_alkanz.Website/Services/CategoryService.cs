using AutoMapper;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;
using the_alkanz.Website.Repositories;

namespace the_alkanz.Website.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDto> CreatAsync(CreateCategoryRequestDto createCategoryRequestDto)
        {
            var category = _mapper.Map<Category>(createCategoryRequestDto);

            category =  await _repository.AddAsync(category);

            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
             var Category = await _repository.DeleteAsync(id);

            if(Category is false) 
                                return false;

            return true;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }
    }
}
