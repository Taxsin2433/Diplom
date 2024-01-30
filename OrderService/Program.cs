using BasketService.Data;
using BasketService.Data.Interfaces;
using BasketService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Data.Interfaces;
using OrderService.Data.Repositories;
using OrderService.Services.Interfaces;
using OrderServiceService = OrderService.Services.OrderService;
using BasketServiceService = OrderService.Services.BasketService;
using IBasketService = OrderService.Services.Interfaces.IBasketService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Конфигурация Entity Framework
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient<IBasketService, BasketServiceService>(); // реализация IBasketService


// Добавление сервисов
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderService, OrderServiceService>();
builder.Services.AddScoped<OrderService.Services.Interfaces.IBasketService, BasketServiceService>();

// Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "OrderServiceInstance";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
