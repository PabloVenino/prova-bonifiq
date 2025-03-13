using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Implementations.Rules.Purchase;

public class CustomerMustBeRegistredRule : IRule<Customer>
{
  private readonly TestDbContext _ctx;
  public CustomerMustBeRegistredRule(TestDbContext ctx)
  {
    _ctx = ctx;
  }

  public async Task<bool> IsSatisfiedAsync(Customer parameters)
  {
    int customerId = parameters.Id;

    var customer = await _ctx.Customers.FindAsync(customerId);

    return customer is not null;
  }
}