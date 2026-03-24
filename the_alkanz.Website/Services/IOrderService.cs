using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public interface IOrderService
{
    public Task<bool> CreatOrderAsync(Guid userId);
    public Task<OrderResponseDto> OrderStatusChangeAsync(Guid userId, OrderStatusChange orderStatus);
    public Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync(Guid userId);
}
