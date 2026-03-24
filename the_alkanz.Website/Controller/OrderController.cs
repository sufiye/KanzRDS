using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public Task<bool> CreatOrderAsync()
    {
        throw new NotImplementedException();
    }
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync()
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public Task<OrderResponseDto> OrderStatusChangeAsync( OrderStatusChange orderStatus)
    {
        throw new NotImplementedException();
    }
}
