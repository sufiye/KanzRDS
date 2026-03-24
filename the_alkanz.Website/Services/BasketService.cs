using AutoMapper;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Repositories;

namespace the_alkanz.Website.Services;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _repository;
    private readonly IMapper _mapper;

    public BasketService(IBasketRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BasketResponseDto> AddToBasketAsync(Guid userId , CreateBasketItemRequestDto createBasketItemRequest)
    {
        var basketItem = await _repository.AddToBasketAsync(userId,createBasketItemRequest);

        if (basketItem == null) return null!;

        return _mapper.Map<BasketResponseDto>(basketItem);
    }

    public async Task<bool> DeleteAsync(Guid id , Guid userId)
    {
        var deleteBasketItem = await _repository.DeleteFromBasketAsync(id,userId);

        if(deleteBasketItem == false)   
                            return false;

        return true;
    }

    public async Task<IEnumerable<BasketResponseDto>> GetAllAsync(Guid userId)
    {
        var basketItems = await _repository.GetBasketItemAsync(userId);

        return _mapper.Map<IEnumerable<BasketResponseDto>>(basketItems);
    }
}
