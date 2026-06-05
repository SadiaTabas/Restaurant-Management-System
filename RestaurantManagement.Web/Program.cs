using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Data;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;

var builder = WebApplication.CreateBuilder(args);
 

 
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RmDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RmDbContext"))
);

 
builder.Services.AddScoped<CustomerRepo>();
builder.Services.AddScoped<MenuItemRepo>();
builder.Services.AddScoped<CategoriesRepo>();
builder.Services.AddScoped<RestaurantTableRepo>();
builder.Services.AddScoped<ReservationRepo>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<AdminRepo>();
builder.Services.AddScoped<TableOrderRepo>();
builder.Services.AddScoped<OrderDetailRepo>();
builder.Services.AddScoped<FeedbackRepo>();
builder.Services.AddScoped<CartItem>();
builder.Services.AddSession();

 

var app = builder.Build();

 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

 
app.UseStaticFiles();

app.UseRouting();
 
app.UseSession();

app.UseAuthorization();

 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();