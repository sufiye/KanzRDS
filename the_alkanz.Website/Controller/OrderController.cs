using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<OrderResponseDto>> CreatOrder()
    {
        var order = await _orderService.CreatOrderAsync(UserId);

        if (order == null)
                        return BadRequest();

        return Ok(order);

    }
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<OrderResponseDto>> GetOrder()
    {
        var orders = await _orderService.GetOrderAsync(UserId);

        if(orders is null)
                    return NotFound();

        return Ok(orders);
    }

    [HttpGet("All")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAllOrder()
    {
        var allOrders = await _orderService.GetAllOrderAsync();

        if (allOrders is null)
                    return NotFound("no orders found !");

        return Ok(allOrders);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OrderResponseDto>> OrderStatusChange(Guid orderId, OrderStatusChange orderStatus)
    {
        var order = await _orderService
                                .OrderStatusChangeAsync(UserId,orderId, orderStatus);

        if (order == null) 
                        return NotFound();

        return Ok(order);

    }
}
