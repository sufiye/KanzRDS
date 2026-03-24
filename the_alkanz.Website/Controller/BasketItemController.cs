using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Services;

namespace the_alkanz.Website.Controller;

[Route("api/[controller]")]
[ApiController]
public class BasketItemController : ControllerBase
{
    private readonly IBasketService _basketService;
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public BasketItemController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<BasketResponseDto>> AddToBasket(CreateBasketItemRequestDto createBasketItemRequest)
    {     

        var basket = await _basketService.AddToBasketAsync(UserId, createBasketItemRequest);

        if (basket == null) return BadRequest();

        return Ok(basket);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<IEnumerable<BasketResponseDto>>> GetAll()
    {
        var basket = await _basketService.GetAllAsync(UserId);

        return Ok(basket);

    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        var basket = await _basketService.DeleteAsync(id,UserId);

        if (basket is false) return NotFound();

        return NoContent();
    }
}
