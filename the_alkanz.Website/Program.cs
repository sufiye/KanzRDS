using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using the_alkanz.Website.Data;
using the_alkanz.Website.Mappings;
using the_alkanz.Website.Models;
using the_alkanz.Website.Repositories;
using the_alkanz.Website.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(
     option =>
     {
         option.SwaggerDoc("v1", new OpenApiInfo
         {
             Version = "v1",
             Title = "the_alkanz API ",
             Description = "the_alkanz API from Sufiya",
             Contact = new OpenApiContact { Name = "the_alkanz  Team", Email = "support@the_alkanz .com" },
             License = new OpenApiLicense { Name = "MIT Licence", Url = new Uri("https://swagger.io/license/") }

         }
         );
     }
    );

var connectionString = builder.
                       Configuration
                       .GetConnectionString("KanzConnectionString");

builder.Services.AddDbContext<KanzDbContext>(
    option => option.UseSqlServer(connectionString)
    );

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<KanzDbContext>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBoxService, BoxService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
           option =>
           {
               option.SwaggerEndpoint("/swagger/v1/swagger.json", "The_alkanz  API v1 ");
               option.RoutePrefix = string.Empty;

               option.EnableTryItOutByDefault();
               option.EnableDeepLinking();
               option.EnableFilter();

           }
        );
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
