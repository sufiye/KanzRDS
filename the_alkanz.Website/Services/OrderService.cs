using AutoMapper;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Repositories;

namespace the_alkanz.Website.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderResponseDto> CreatOrderAsync(Guid userId)
    {
        var order = await _orderRepository.CreatOrderAsync(userId);

        if (order == null)
                    return null!;

        return _mapper.Map<OrderResponseDto>(order);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync()
    {
        var allOrders = await _orderRepository.GetAllOrderAsync();

        if (allOrders == null)
                             return null!;

        return _mapper.Map<IEnumerable<OrderResponseDto>>(allOrders);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetOrderUserAllAsync(Guid userId)
    {
        var order = await _orderRepository.GetOrderUserAllAsync(userId);

        if (order  is null) 
                        return null!;

        return _mapper.Map<IEnumerable<OrderResponseDto>>(order);
    }

    public async Task<OrderResponseDto> OrderStatusChangeAsync( Guid orderId, OrderStatusChange orderStatus)
    {
        var order = await _orderRepository
                                .OrderStatusChangeAsync( orderId, orderStatus);

        if (order == null) 
                        return null!;

        return _mapper.Map<OrderResponseDto>(order);
    }
}
