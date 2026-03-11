using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public interface IOrderService
{
    public Task<bool> CreatOrderAsync();
    public Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync();
}
