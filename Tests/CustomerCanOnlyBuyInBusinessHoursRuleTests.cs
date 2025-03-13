using Microsoft.AspNetCore.Authentication;
using Moq;
using ProvaPub.Implementations.Rules.Purchase;
using ProvaPub.Models;
using Xunit;

namespace ProvaPub.Tests
{
  public class CustomerCanOnlyBuyInBusinessHoursRuleTests
  {
    [Theory]
    [InlineData(9, 55, 59, DayOfWeek.Monday, true)]
    [InlineData(17, 59, 59, DayOfWeek.Wednesday, true)]
    [InlineData(18, 00, 01, DayOfWeek.Thursday, false)]
    [InlineData(18, 00, 00, DayOfWeek.Tuesday, false)]
    [InlineData(07, 59, 58, DayOfWeek.Friday, false)]
    [InlineData(07, 59, 59, DayOfWeek.Friday, false)]
    [InlineData(09, 30, 00, DayOfWeek.Monday, true)]
    [InlineData(09, 30, 00, DayOfWeek.Saturday, false)]
    [InlineData(10, 30, 00, DayOfWeek.Sunday, false)]
    public async Task CustomerCanOnlyBuyInBusinessHours_PurchaseAllowedOnBusinessHours(
      int hour, int minute, int second, DayOfWeek dayOfTheWeek, bool expectedResult)
    {
      // Arrenge
      var customer = new Customer { Id = 1 };
      var mockClock = new Mock<ISystemClock>();
      var fakeTime = new DateTime(2025, 03, 13, hour+3, minute, second);
      fakeTime = fakeTime.AddDays(dayOfTheWeek - fakeTime.DayOfWeek);
      
      mockClock.Setup(m => m.UtcNow).Returns(fakeTime);

      var rule = new CustomerCanOnlyBuyInBusinessHours(mockClock.Object);

      // Act
      var result = await rule.IsSatisfiedAsync(customer);

      // Assert
      Assert.Equal(result, expectedResult);

    }
  }
}
