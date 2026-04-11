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
        var product = await _context.Products.Include(b =>b.Attachments)
                                .FirstOrDefaultAsync(x => x.Id == createBasketItem.ProductId);

        var basketItem = await _context.BasketItems
                                .FirstOrDefaultAsync(x => x.ProductId == createBasketItem.ProductId 
                                                                               && x.UserId == userId);

        if (basketItem != null)
        {
            if (basketItem.Quantity + createBasketItem.Quantity > product!.StockCount) return null!; 

            basketItem.Quantity += createBasketItem.Quantity;
        }
        else
        {
            if (createBasketItem.Quantity > product!.StockCount) return null!;

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

    public async Task<IEnumerable<BasketResponseDto>> GetBasketItemAsync(Guid userId)
    {
        var basketItems = await _context.BasketItems
            .Where(x => x.UserId == userId)
            .Include(x => x.Product)
            .ThenInclude(x => x.Attachments)
            .ToListAsync();

        var invalidItems = basketItems
            .Where(x => x.Product == null)
            .ToList();

        if (invalidItems.Any())
        {
            _context.BasketItems.RemoveRange(invalidItems);
            await _context.SaveChangesAsync();

            basketItems = basketItems
                .Except(invalidItems)
                .ToList();
        }

        return _mapper.Map<IEnumerable<BasketResponseDto>>(basketItems);
    }
}
