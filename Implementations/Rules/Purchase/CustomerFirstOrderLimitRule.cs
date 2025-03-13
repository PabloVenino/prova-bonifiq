using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Implementations.Rules.Purchase;

public class CustomerFirstOrderLimitRule : IRule<(Customer, decimal)>
{
  private readonly TestDbContext _ctx;
  private readonly decimal _limit = 100.00M;

  public CustomerFirstOrderLimitRule(TestDbContext ctx)
  {
    _ctx = ctx;
  }

  public async Task<bool> IsSatisfiedAsync((Customer, decimal) input)
  {
    var (customer, purchaseValue) = input;

    var haveBoughtBefore = await _ctx.Orders.AnyAsync(o => o.CustomerId == customer.Id);

    return haveBoughtBefore || purchaseValue <= _limit; // TODO: Solve the purchaseValue problem
  }
}