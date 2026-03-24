using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Repositories;

public interface IOrderRepository
{
    public Task<OrderResponseDto> CreatOrderAsyc(Guid userId);
    public Task<IEnumerable<OrderResponseDto>> GetOrderAsync(Guid userId);
    public Task<OrderResponseDto> OrderStatusChangeAsync(Guid userId,OrderStatusChange orderStatus);
}
