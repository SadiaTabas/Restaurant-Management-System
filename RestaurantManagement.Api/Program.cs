using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Data;
using RestaurantManagement.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<RmDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RmDbContext"))
);

// Repositories
builder.Services.AddScoped<CustomerRepo>();
builder.Services.AddScoped<MenuItemRepo>();
builder.Services.AddScoped<CategoriesRepo>();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
