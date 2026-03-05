using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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
