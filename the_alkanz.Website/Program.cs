using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
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

         var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
         var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile) ;

         if(File.Exists(xmlPath)) option.IncludeXmlComments(xmlPath) ;

         option.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
         {
             Description = "JWT Authorization header using Bearer scheme. \r\n\r\nExample: Bearer {token}",
             Name = "Authorization",
             In = ParameterLocation.Header,
             Type = SecuritySchemeType.ApiKey,
             Scheme = "Bearer"
         });

         option.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
             {
                 new OpenApiSecurityScheme
                 {
                     Reference = new OpenApiReference
                     {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                     }
                 },new string[]{}
             }
         });
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

var jwtSettings = builder.Configuration.GetSection("JWTSettings");
var secretKey = jwtSettings["SecretKey"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                ClockSkew = TimeSpan.Zero
            };
        }
    );
builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();



builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await RoleSeeder.SeedRoleAsync(services);
    }
    catch (Exception  ex )
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured while seeding roles");
    }
}

app.Run();
