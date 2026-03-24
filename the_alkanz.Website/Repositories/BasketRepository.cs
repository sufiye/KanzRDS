using AutoMapper;
using Microsoft.EntityFrameworkCore;
using the_alkanz.Website.Data;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Repositories;

public class BasketRepository : IBasketRepository
{
    private  readonly KanzDbContext _context;
    private readonly IMapper _mapper;

    public BasketRepository(KanzDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BasketResponseDto> AddToBasketAsync(Guid userId, CreateBasketItemRequestDto  createBasketItem)
    {
        var basketItem = await _context.BasketItems
            .FirstOrDefaultAsync(x => x.ProductId == createBasketItem.ProductId && x.UserId == userId);

        if (basketItem != null)
        {
            basketItem.Quantity += createBasketItem.Quantity;
        }
        else
        {
            basketItem = new BasketItem
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ProductId = createBasketItem.ProductId,
                Quantity = createBasketItem.Quantity
            };

            await _context.BasketItems.AddAsync(basketItem);
        }

        await _context.SaveChangesAsync();

        return _mapper.Map<BasketResponseDto>(basketItem);
    }

    public async Task<bool> DeleteFromBasketAsync(Guid id,Guid userId)
    {
        var basket = await _context.BasketItems.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

        if (basket == null) 
                    return false!;

        _context.BasketItems.Remove(basket);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<BasketResponseDto>> GetBasketItemAsync(Guid UserId)
    {
        var basketItems = await _context.BasketItems
            .Where(x => x.UserId == UserId)
            .Include(x => x.Product)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BasketResponseDto>>(basketItems);
    }
}
