using ProvaPub.Interfaces;
using ProvaPub.Models;

namespace ProvaPub.Services
{
  public class ProductService : IProductService
  {
    private readonly IServiceAbstractions<Product> _ServiceImplementation;

    public ProductService(IServiceAbstractions<Product> ServiceImplementation)
    {
      _ServiceImplementation = ServiceImplementation;
    }

    public async Task<PaginateItem<Product>> GetPaginatedProductAsync(int pageNumber, CancellationToken cancellation)
    {
      return await _ServiceImplementation.GetPaginatedEntityAsync(pageNumber, cancellation);
    }
  }
}
