using ProvaPub.Models;

namespace ProvaPub.Interfaces;

public interface ICustomerService
{
  Task<PaginateItem<Customer>> GetPaginatedCustomerAsync(int pageNumber, CancellationToken cancellationToken);
}