using AutoMapper;
using Microsoft.EntityFrameworkCore;
using the_alkanz.Website.Data;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Repositories;

public class OrderRepository : IOrderRepository  
{
    private readonly KanzDbContext _context;
    private readonly IMapper _mapper;
    public OrderRepository(KanzDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderResponseDto> CreatOrderAsync(Guid userId)
    {
       var basketItems = await _context
                                    .BasketItems
                                    .Include(p => p.Product)
                                    .Where(x => x.UserId == userId)
                                    .ToListAsync();

        if (basketItems.Any() is false)
                            return null!;

        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            OrderDate = DateTimeOffset.Now,
            Status = OrderStatus.Pending
        };

        var orderItems = new List<OrderItem>();

        foreach (var item in basketItems)
        {
            orderItems.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = item.ProductId,
                Price = item.Product.Price,
                Quantity = item.Quantity
            });
        }

        order.OrderItems = orderItems;
        order.TotalPrice = orderItems.Sum(x => x.Price * x.Quantity);

        await _context.Orders.AddAsync(order);

        _context.BasketItems.RemoveRange(basketItems);

        await _context.SaveChangesAsync();

        return _mapper.Map<OrderResponseDto>(order);

    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync()
    {
        var allOrders = await _context
                                    .Orders
                                    .Include(x => x.OrderItems)
                                    .ToListAsync();

        if (allOrders.Any() is false)
                            return null!;

        return _mapper.Map<IEnumerable<OrderResponseDto>>(allOrders);                   

    }

    public async Task<IEnumerable<OrderResponseDto>> GetOrderUserAllAsync(Guid userId)
    {
        var order = await _context
                                .Orders
                                .Include(x => x.OrderItems)
                                .Where(x => x.UserId == userId)
                                .ToListAsync();
        if (order == null)
                        return null!;

        return _mapper.Map<IEnumerable<OrderResponseDto>>(order);   
    }

    public async Task<OrderResponseDto> OrderStatusChangeAsync(Guid orderId, OrderStatusChange orderStatus)
    {
        var order = await _context
                                .Orders
                                .Include(x => x.OrderItems)
                                .FirstOrDefaultAsync(o => o.Id == orderId);

        if(order == null) 
                    return null!;

        _mapper.Map(orderStatus, order);

        await _context.SaveChangesAsync();

        return _mapper.Map<OrderResponseDto>(order);

    }
}
