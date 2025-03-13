using Microsoft.EntityFrameworkCore;
using ProvaPub.Implementations.Rules.Purchase;
using ProvaPub.Models;
using ProvaPub.Repository;
using Xunit;

namespace ProvaPub.Tests
{
  public class CustomerMustBeRegisteredRuleTests
  {
    private readonly TestDbContext _ctx;

    public CustomerMustBeRegisteredRuleTests()
    {
      var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase1")
            .Options;

      _ctx = new TestDbContext(options);
    }

    [Fact]
    public async Task CustomerMustBeRegistered_CanPurchase()
    {
      // Arrange
      var rule = new CustomerMustBeRegistredRule(_ctx);
      var customer = new Customer { Id = 1, Name = "Pablo" };
      _ctx.Customers.Add(customer);
      await _ctx.SaveChangesAsync();

      // Act
      var result = await rule.IsSatisfiedAsync(customer);

      // Assert
      Assert.True(result);
    }

    [Fact]
    public async Task CustomerMustBeRegistered_UnregisteredUser()
    {
      // Arrange
      var rule = new CustomerMustBeRegistredRule(_ctx);
      var customer = new Customer { Id = 2, Name = "Pablo 2" };

      // Act
      var result = await rule.IsSatisfiedAsync(customer);

      // Assert
      Assert.False(result);
    }
  }
}
