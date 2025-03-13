using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
  public class CustomerService : ICustomerService
  {
    private readonly IServiceAbstractions<Customer> _ServiceImplementation;
    public CustomerService(IServiceAbstractions<Customer> ServiceImplementation)
    {
      _ServiceImplementation = ServiceImplementation;
    }

    public async Task<PaginateItem<Customer>> GetPaginatedCustomerAsync(int pageNumber, CancellationToken cancellationToken)
    {
      return await _ServiceImplementation.GetPaginatedEntityAsync(pageNumber, cancellationToken);
    }

    public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
    {
      return await _ServiceImplementation.IsValidAsync(new Customer { Id = customerId });
    }
  }
}
