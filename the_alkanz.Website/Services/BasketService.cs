using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public class BasketService : IBasketService
{
    public Task<BasketResponseDto> AddToBasketAsync(CreateBasketItemRequestDto createBasketItemRequest)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BasketResponseDto>> GetAll()
    {
        throw new NotImplementedException();
    }
}
