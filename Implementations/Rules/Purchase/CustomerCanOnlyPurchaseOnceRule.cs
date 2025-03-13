using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Implementations.Rules.Purchase;

public class CustomerCanOnlyPurchaseOnceRule : IRule<Customer>
{
  private readonly TestDbContext _ctx;
  public CustomerCanOnlyPurchaseOnceRule(TestDbContext ctx)
  {
    _ctx = ctx;
  }

  public async Task<bool> IsSatisfiedAsync(Customer customer)
  {
    var baseDate = DateTime.UtcNow.AddMonths(-1);
    var ordersInThisMonth = await _ctx.Orders.CountAsync(o => o.CustomerId == customer.Id && o.OrderDate >= baseDate);
    return ordersInThisMonth == 0;
  }
}