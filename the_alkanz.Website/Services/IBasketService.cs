using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public interface IBasketService
{
    public Task<BasketResponseDto> AddToBasketAsync(CreateBasketItemRequestDto createBasketItemRequest);
    public Task<IEnumerable<BasketResponseDto>> GetAll();
    public Task<bool> Delete(Guid id);
}
