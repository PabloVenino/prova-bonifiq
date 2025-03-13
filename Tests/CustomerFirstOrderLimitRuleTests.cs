using Microsoft.EntityFrameworkCore;
using ProvaPub.Implementations.Rules.Purchase;
using ProvaPub.Models;
using ProvaPub.Repository;
using Xunit;

namespace ProvaPub.Tests
{
  public class CustomerFirstOrderLimitRuleTests
  {
    private readonly TestDbContext _ctx;

    public CustomerFirstOrderLimitRuleTests()
    {
      var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase1")
            .Options;

      _ctx = new TestDbContext(options);
    }

    [Fact]
    public async Task CustomerFirstOrderLimitRule_WithinLimit_CanPurchase()
    {
      // Arrange
      var rule = new CustomerFirstOrderLimitRule(_ctx);
      var customer = new Customer { Id = 1, Name = "Pablo" };
      decimal purchaseValue = 90m;
      _ctx.Customers.Add(customer);
      await _ctx.SaveChangesAsync();

      // Act
      var result = await rule.IsSatisfiedAsync((customer, purchaseValue));

      // Assert
      Assert.True(result);
    }

    [Fact]
    public async Task CustomerFirstOrderLimitRule_IsNotFirstPurchase_CanPurchase()
    {
      // Arrange
      var rule = new CustomerFirstOrderLimitRule(_ctx);
      var customer = new Customer { Id = 2, Name = "Pablo 2" };
      var order = new Order
      {
        CustomerId = 2,
        OrderDate = DateTime.UtcNow.AddDays(-5),
        Id = 1,
        Value = 190m
      };
      _ctx.Customers.Add(customer);
      _ctx.Orders.Add(order);
      await _ctx.SaveChangesAsync();

      // Act
      var result = await rule.IsSatisfiedAsync((customer, 190m));

      // Assert
      Assert.True(result);
    }

    [Fact]
    public async Task CustomerFirstOrderLimitRule_OverTheLimit_CannotPurchase()
    {
      // Arrange
      var rule = new CustomerFirstOrderLimitRule(_ctx);
      var customer = new Customer { Id = 3, Name = "Pablo 3" };
      decimal purchaseValue = 100.01m;
      _ctx.Customers.Add(customer);
      await _ctx.SaveChangesAsync();

      // Act
      var result = await rule.IsSatisfiedAsync((customer, purchaseValue));

      // Assert
      Assert.False(result);
    }
  }
}
