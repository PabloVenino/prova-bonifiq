
using ProvaPub.Models;

namespace ProvaPub.Interfaces;

public interface IProductService
{
  Task<PaginateItem<Product>> GetPaginatedProductAsync(int pageNumber, CancellationToken cancellationToken);
}