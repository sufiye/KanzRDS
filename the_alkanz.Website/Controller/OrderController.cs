using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

/// <summary>
/// Handles operations related to orders.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    /// <summary>
    /// Gets the currently authenticated user's Id from claims.
    /// </summary>
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderController"/> class.
    /// </summary>
    /// <param name="orderService">Service responsible for order operations.</param>
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Creates a new order for the authenticated user based on their basket items.
    /// </summary>
    /// <returns>The created order.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<OrderResponseDto>> CreatOrder()
    {
        var order = await _orderService.CreatOrderAsync(UserId);

        if (order == null)
            return BadRequest();

        return Ok(order);
    }

    /// <summary>
    /// Retrieves all orders belonging to the authenticated user.
    /// </summary>
    /// <returns>A list of user's orders.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<OrderResponseDto>> GetOrder()
    {
        var orders = await _orderService.GetOrderAsync(UserId);

        if (orders is null)
            return NotFound();

        return Ok(orders);
    }

    /// <summary>
    /// Retrieves all orders in the system.
    /// Only accessible by administrators.
    /// </summary>
    /// <returns>A list of all orders.</returns>
    [HttpGet("All")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAllOrder()
    {
        var allOrders = await _orderService.GetAllOrderAsync();

        if (allOrders is null)
            return NotFound("No orders found!");

        return Ok(allOrders);
    }

    /// <summary>
    /// Updates the status of a specific order.
    /// Only administrators can change order status.
    /// </summary>
    /// <param name="orderId">The Id of the order to update.</param>
    /// <param name="orderStatus">The new status of the order.</param>
    /// <returns>The updated order.</returns>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OrderResponseDto>> OrderStatusChange(Guid orderId, OrderStatusChange orderStatus)
    {
        var order = await _orderService
            .OrderStatusChangeAsync(UserId, orderId, orderStatus);

        if (order == null)
            return NotFound();

        return Ok(order);
    }
}