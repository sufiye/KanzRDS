using the_alkanz.Website.DTOs;
namespace the_alkanz.Website.Repositories;

public interface IOrderRepository
{
    public Task<OrderResponseDto> CreatOrderAsync(Guid userId);
    public Task<IEnumerable<OrderResponseDto>> GetOrderUserAllAsync(Guid userId);
    public Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync();
    public Task<OrderResponseDto> OrderStatusChangeAsync(Guid orderId, OrderStatusChange orderStatus);
}
