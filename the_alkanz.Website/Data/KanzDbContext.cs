using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Data;

public class KanzDbContext : IdentityDbContext<ApplicationUser>
{
    public KanzDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Product> Products  => Set<Product>();
    public DbSet<BasketItem>  BasketItems => Set<BasketItem>();
    public DbSet<Order>  Orders => Set<Order>();
    public DbSet<OrderItem>  OrderItems => Set<OrderItem>();
    public DbSet<Category>  Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>().
            HasOne(p=>p.Category).
            WithMany(p=>p.Products).
            HasForeignKey(p=>p.CategoryId);

        
    }
}
