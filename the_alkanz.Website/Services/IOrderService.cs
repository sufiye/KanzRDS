using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public interface IOrderService
{
    public Task<OrderResponseDto> CreatOrderAsync(Guid userId);
    public Task<OrderResponseDto> OrderStatusChangeAsync(Guid orderId, OrderStatusChange orderStatus);
    public Task<IEnumerable<OrderResponseDto>> GetOrderUserAllAsync(Guid userId);
    public Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync();
}
