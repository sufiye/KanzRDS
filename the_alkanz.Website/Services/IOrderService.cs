using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public interface IOrderService
{
    public Task<OrderResponseDto> CreatOrderAsync(Guid userId);
    public Task<OrderResponseDto> OrderStatusChangeAsync(Guid userId, Guid orderId, OrderStatusChange orderStatus);
    public Task<OrderResponseDto> GetOrderAsync(Guid userId);
    public Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync();
}
