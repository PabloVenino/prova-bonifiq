using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
  public class ProductService : IProductService
  {
    private readonly IServiceHelper<Product> _serviceHelper;

    public ProductService(IServiceHelper<Product> serviceHelper)
    {
      _serviceHelper = serviceHelper;
    }

    public async Task<PaginateItem<Product>> GetPaginatedProductAsync(int pageNumber, CancellationToken cancellation)
    {
      return await _serviceHelper.GetPaginatedEntityAsync(pageNumber, cancellation);
    }
  }

  public interface IProductService
  {
    Task<PaginateItem<Product>> GetPaginatedProductAsync(int pageNumber, CancellationToken cancellationToken);
  }
}
