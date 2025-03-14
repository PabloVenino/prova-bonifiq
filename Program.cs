using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Handlers;
using ProvaPub.Implementations;
using ProvaPub.Implementations.Rules.Purchase;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<RandomService>();
builder.Services.AddTransient(typeof(IServiceAbstractions<>), typeof(ServiceImplementation<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRule<Customer>, CustomerMustBeRegistredRule>();
builder.Services.AddScoped<IRule<Customer>, CustomerCanOnlyBuyInBusinessHours>();
builder.Services.AddScoped<IRule<(Customer, decimal)>, CustomerFirstOrderLimitRule>();
builder.Services.AddScoped<IRule<Customer>, CustomerCanOnlyPurchaseOnceRule>();
builder.Services.AddTransient<ISystemClock, SystemClock>();

builder.Services.AddDbContext<TestDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
