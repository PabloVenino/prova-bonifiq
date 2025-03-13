using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Implementations.Rules.Purchase;

public class CustomerCanOnlyBuyInBusinessHours : IRule<Customer>
{
  private readonly TestDbContext _ctx;
  private const int UtcMinusThree = -3;

  public CustomerCanOnlyBuyInBusinessHours(TestDbContext ctx)
  {
    _ctx = ctx;
  }

  public async Task<bool> IsSatisfiedAsync(Customer customer)
  {
    var now = DateTime.UtcNow.AddHours(UtcMinusThree);
    bool isBusinessHours = now.Hour >= 8 && now.Hour <= 18 &&
                            now.DayOfWeek != DayOfWeek.Saturday &&
                            now.DayOfWeek != DayOfWeek.Sunday;

    return isBusinessHours;
  }
}
