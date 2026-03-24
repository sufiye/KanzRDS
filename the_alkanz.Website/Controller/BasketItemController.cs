using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

/// <summary>
/// Manages basket operations for authenticated users.
/// Allows users to add products to their basket, view basket items, and remove items.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BasketItemController : ControllerBase
{
    private readonly IBasketService _basketService;

    /// <summary>
    /// Gets the current authenticated user's Id from the JWT token.
    /// </summary>
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>
    /// Initializes a new instance of the <see cref="BasketItemController"/> class.
    /// </summary>
    /// <param name="basketService">Service responsible for basket operations.</param>
    public BasketItemController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    /// <summary>
    /// Adds a product to the current user's basket.
    /// If the product already exists in the basket, the quantity will be increased.
    /// </summary>
    /// <param name="createBasketItemRequest">Basket item creation request containing product and quantity.</param>
    /// <returns>The created or updated basket item.</returns>
    /// <response code="200">Basket item successfully added or updated.</response>
    /// <response code="400">Invalid request.</response>
    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<BasketResponseDto>> AddToBasket(CreateBasketItemRequestDto createBasketItemRequest)
    {
        var basket = await _basketService.AddToBasketAsync(UserId, createBasketItemRequest);

        if (basket == null) return BadRequest();

        return Ok(basket);
    }

    /// <summary>
    /// Retrieves all basket items for the currently authenticated user.
    /// </summary>
    /// <returns>A list of basket items belonging to the user.</returns>
    /// <response code="200">Returns the user's basket items.</response>
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<IEnumerable<BasketResponseDto>>> GetAll()
    {
        var basket = await _basketService.GetAllAsync(UserId);
        return Ok(basket);
    }

    /// <summary>
    /// Removes a basket item by its identifier.
    /// The item must belong to the authenticated user.
    /// </summary>
    /// <param name="id">Basket item identifier.</param>
    /// <returns>No content if deletion is successful.</returns>
    /// <response code="204">Basket item successfully deleted.</response>
    /// <response code="404">Basket item not found.</response>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        var basket = await _basketService.DeleteAsync(id, UserId);

        if (basket is false) return NotFound();

        return NoContent();
    }
}