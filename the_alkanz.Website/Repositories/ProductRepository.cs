using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using the_alkanz.Website.common;
using the_alkanz.Website.Data;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly KanzDbContext _context;

    public ProductRepository(KanzDbContext context)
    {
        _context = context;
    }

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var productDelete = await  _context.Products.FirstOrDefaultAsync(p => p.Id == Id);

        if (productDelete is null) return false;

        _context.Products.Remove(productDelete);

        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = await _context.Products.Include(p=>p.Attachments).ToListAsync();

        return products;

    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        var product = await _context.Products.Include(p => p.Attachments).FirstOrDefaultAsync(p=>p.Id == id);

        if (product is null) return null!;

        return product!;
    }

    public async Task<PageResult<Product>> GetPagedAsync(ProductQueryParams productQueryParams)
    {
        productQueryParams.Validate();

        var query = _context.Products.Include(p => p.Attachments).AsQueryable();

        if (!string.IsNullOrEmpty(productQueryParams.Search))
        {
            var searchTerm = productQueryParams.Search.ToLower();
            query = query.Where(p =>
                p.Name.ToLower().Contains(searchTerm)
            );
        }
        if (!string.IsNullOrEmpty(productQueryParams.SearchTittle))
        {
            var searchTerm = productQueryParams.SearchTittle.ToLower();
            query = query.Where(p =>  
                p.Title.ToLower().Contains(searchTerm)     
            );
        }
        if (!string.IsNullOrEmpty(productQueryParams.SearchDescription))
        {
            var searchTerm = productQueryParams.SearchDescription.ToLower();
            query = query.Where(p =>
                p.Description.ToLower().Contains(searchTerm)
            );
        }

        if (!string.IsNullOrEmpty(productQueryParams.Sort))
        {
            query = ApplySorting(query, productQueryParams.Sort, productQueryParams.SortDirection);
        }
        else
        {
            query = query.OrderBy(p => p.Id);
        }

        var totalCount = await query.CountAsync();
        var skip = (productQueryParams.Page - 1) * productQueryParams.PageSize;
        var itemsSkip = await query.Skip(skip).Take(productQueryParams.PageSize).ToListAsync();

 
        return PageResult<Product>.Creat(
            itemsSkip,
            productQueryParams.Page,
            productQueryParams.PageSize,
            totalCount
        );
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId)
    {
        var products = await _context.Products.Include(p=>p.Attachments).Where(p=>p.CategoryId == categoryId).ToListAsync();

        return products!;
    }

    public async Task UpdateAsync(Product product)
    {
        
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    private IQueryable<Product> ApplySorting(IQueryable<Product> query, string sort, string? sortDirection)
    {
        var isDesc = sortDirection?.ToLower() == "desc";

        return sort.ToLower() switch
        {

            "name" => isDesc ?
                                query.OrderByDescending(p => p.Name) : 
                                query.OrderBy(p => p.Name),

            "title" => isDesc ?
                                query.OrderByDescending(p => p.Title) : 
                                query.OrderBy(p => p.Title),
            "description" => isDesc ? 
                                query.OrderByDescending(p => p.Description) :
                                query.OrderBy(p => p.Description),

            "price" => isDesc ? 
                                query.OrderByDescending(p => p.Price) :
                                query.OrderBy(p => p.Price),

            "stockcount" => isDesc ?
                                query.OrderByDescending(p => p.StockCount) :
                                query.OrderBy(p => p.StockCount),

            "createdat" => isDesc ? 
                                query.OrderByDescending(p => p.CreatedAt) : 
                                query.OrderBy(p => p.CreatedAt),

            "updatedat" => isDesc ? 
                                query.OrderByDescending(p => p.UpdatedAt) : 
                                query.OrderBy(p => p.UpdatedAt),

            _ => query.OrderBy(p => p.Id)
        };
    }

}
