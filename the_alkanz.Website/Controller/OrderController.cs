using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

/// <summary>
/// Provides API endpoints for managing user and administrative order operations.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    /// <summary>
    /// Gets the unique identifier of the currently authenticated user from JWT claims.
    /// </summary>
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderController"/> class.
    /// </summary>
    /// <param name="orderService">Service responsible for handling order-related business logic.</param>
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Creates a new order for the authenticated user based on their current basket items.
    /// </summary>
    /// <remarks>
    /// Accessible by users and administrators.
    /// </remarks>
    /// <returns>
    /// Returns the created order if successful; otherwise, returns a BadRequest response.
    /// </returns>
    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<OrderResponseDto>> CreateOrder()
    {
        var order = await _orderService.CreatOrderAsync(UserId);

        if (order == null)
            return BadRequest("Order could not be created.");

        return Ok(order);
    }

    /// <summary>
    /// Retrieves all orders belonging to the currently authenticated user.
    /// </summary>
    /// <remarks>
    /// Accessible by users and administrators.
    /// </remarks>
    /// <returns>
    /// A list of orders associated with the user. Returns NotFound if no orders exist.
    /// </returns>
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetUserOrdersAsync()
    {
        var orders = await _orderService.GetOrderUserAllAsync(UserId);

        if (orders == null || !orders.Any())
            return NotFound("No orders found for the current user.");

        return Ok(orders);
    }

    /// <summary>
    /// Retrieves all orders in the system.
    /// </summary>
    /// <remarks>
    /// This endpoint is restricted to administrators only.
    /// </remarks>
    /// <returns>
    /// A list of all orders. Returns NotFound if no orders exist.
    /// </returns>
    [HttpGet("All")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAllOrdersAsync()
    {
        var allOrders = await _orderService.GetAllOrderAsync();

        if (allOrders == null || !allOrders.Any())
            return NotFound("No orders found.");

        return Ok(allOrders);
    }

    /// <summary>
    /// Updates the status of a specific order.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <param name="orderStatus">The new status to be assigned to the order.</param>
    /// <remarks>
    /// Only administrators are authorized to change order status.
    /// </remarks>
    /// <returns>
    /// The updated order if successful; otherwise, returns NotFound if the order does not exist.
    /// </returns>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OrderResponseDto>> ChangeOrderStatus(Guid id, OrderStatusChange orderStatus)
    {
        var order = await _orderService.OrderStatusChangeAsync(id, orderStatus);

        if (order == null)
            return NotFound("Order not found.");

        return Ok(order);
    }
}