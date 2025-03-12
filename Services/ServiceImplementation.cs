using System.Threading;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services;

public class ServiceImplementation : IServiceHelper
{
  private readonly TestDbContext _ctx;

  public ServiceImplementation(TestDbContext ctx)
  {
    _ctx = ctx;
  }

  public async Task<PaginateItem<Product>> GetPaginatedProductsAsync(
    int pageNumber, CancellationToken cancellationToken
  )
  {
    return await _ctx.Products
                      .OrderBy(p => p.Name)
                      .ToPagedResultAsync(pageNumber, cancellationToken);
  }
}

public interface IServiceHelper
{
  Task<PaginateItem<Product>> GetPaginatedProductsAsync(int pageNumber, CancellationToken cancellationToken);
}