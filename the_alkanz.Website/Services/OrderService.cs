using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public class OrderService : IOrderService
{
    public Task<bool> CreatOrderAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDto> OrderStatusChangeAsync(Guid userId, OrderStatusChange orderStatus)
    {
        throw new NotImplementedException();
    }
}
