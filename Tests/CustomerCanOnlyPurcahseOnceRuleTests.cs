using Microsoft.EntityFrameworkCore;
using ProvaPub.Implementations.Rules.Purchase;
using ProvaPub.Models;
using ProvaPub.Repository;
using Xunit;

namespace ProvaPub.Tests
{
  public class CustomerCanOnlyPurcahseOnceRuleTests
  {
    private readonly TestDbContext _ctx;

    public CustomerCanOnlyPurcahseOnceRuleTests()
    {
      var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase1")
            .Options;

      _ctx = new TestDbContext(options);
    }

    [Fact]
    public async Task CustomerCanOnlyPurcahseOnceRule_CanPurchaseInCurrentMonth()
    {
      // Arrange
      var rule = new CustomerCanOnlyPurchaseOnceRule(_ctx);
      var customer = new Customer { Id = 1, Name = "Pablo" };

      var order = new Order
      {
        CustomerId = 1,
        Id = 1,
        OrderDate = DateTime.UtcNow.AddMonths(-2),
        Value = 100
      };

      _ctx.Customers.Add(customer);
      _ctx.Orders.Add(order);
      await _ctx.SaveChangesAsync();

      // Act
      var result = await rule.IsSatisfiedAsync(customer);

      // Assert
      Assert.True(result);
    }

    [Fact]
    public async Task CustomerCanOnlyPurcahseOnceRule_CannotPurchaseInCurrentMonth()
    {
      // Arrange
      var rule = new CustomerCanOnlyPurchaseOnceRule(_ctx);
      var customer = new Customer { Id = 2, Name = "Pablo 2" };
      var order = new Order
      {
        CustomerId = 2,
        Id = 2,
        Value = 99,
        OrderDate = DateTime.UtcNow.AddDays(-5)
      };
      _ctx.Customers.Add(customer);
      _ctx.Orders.Add(order);
      await _ctx.SaveChangesAsync();

      // Act
      var result = await rule.IsSatisfiedAsync(customer);

      // Assert
      Assert.False(result);
    }
  }
}
