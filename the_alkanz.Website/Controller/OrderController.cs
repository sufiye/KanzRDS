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
    public async Task<ActionResult<OrderResponseDto>> CreatOrderAsync()
    {
        var order = await _orderService.CreatOrderAsync(UserId);

        if (order == null)
                        return BadRequest();

        return Ok(order);

    }
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<OrderResponseDto>> GetAllOrderAsync()
    {
        var orders = await _orderService.GetAllOrderAsync(UserId);

        if(orders is null)
                    return NotFound();

        return Ok(orders);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OrderResponseDto>> OrderStatusChangeAsync(Guid orderId, OrderStatusChange orderStatus)
    {
        var order = await _orderService
                                .OrderStatusChangeAsync(UserId,orderId, orderStatus);

        if (order == null) 
                        return NotFound();

        return Ok(order);

    }
}
