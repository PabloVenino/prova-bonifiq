using Microsoft.AspNetCore.Authentication;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Implementations.Rules.Purchase;

public class CustomerCanOnlyBuyInBusinessHours : IRule<Customer>
{
  private const int UtcMinusThree = -3;
  private readonly ISystemClock _clock;

  public CustomerCanOnlyBuyInBusinessHours(ISystemClock clock)
  {
    _clock = clock;
  }

  public async Task<bool> IsSatisfiedAsync(Customer customer)
  {
    var now = _clock.UtcNow.AddHours(UtcMinusThree);
    bool isBusinessHours = now.Hour >= 8 && now.Hour < 18 &&
                            now.DayOfWeek != DayOfWeek.Saturday &&
                            now.DayOfWeek != DayOfWeek.Sunday;

    return isBusinessHours;
  }
}
