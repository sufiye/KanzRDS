using the_alkanz.Website.DTOs;
namespace the_alkanz.Website.Repositories;

public interface IOrderRepository
{
    public Task<OrderResponseDto> CreatOrderAsync(Guid userId);
    public Task<OrderResponseDto> GetOrderAsync(Guid userId);
    public Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync();
    public Task<OrderResponseDto> OrderStatusChangeAsync(Guid userId, Guid orderId, OrderStatusChange orderStatus);
}
