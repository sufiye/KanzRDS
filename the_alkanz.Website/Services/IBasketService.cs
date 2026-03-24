using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public interface IBasketService
{
    public Task<BasketResponseDto> AddToBasketAsync(Guid userId ,CreateBasketItemRequestDto createBasketItemRequest);
    public Task<IEnumerable<BasketResponseDto>> GetAllAsync(Guid userId);
    public Task<bool> DeleteAsync(Guid id,Guid userId);
}
