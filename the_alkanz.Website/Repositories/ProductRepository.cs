using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        var product = await _context.Products.FirstOrDefaultAsync(p=>p.Id == id);

        if (product is null) return null!;

        return product!;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId)
    {
        var products = await _context.Products.Where(p=>p.CategoryId == categoryId).ToListAsync();

        return products!;
    }

    public async Task UpdateAsync(Product product)
    {
        
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

}
