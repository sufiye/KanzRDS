using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Repositories;

public interface IBasketRepository
{
    public Task<BasketResponseDto> AddToBasketAsync(Guid userId, CreateBasketItemRequestDto createBasketItem);
    public Task<IEnumerable<BasketResponseDto>> GetBasketItemAsync(Guid UserId);
    public Task<bool> DeleteFromBasketAsync(Guid id,Guid userId);
}
