using System.Threading;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Extensions;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services;

public class ServiceHelper<T> : IServiceHelper<T> where T : class
{
  private readonly TestDbContext _ctx;

  public ServiceHelper(TestDbContext ctx)
  {
    _ctx = ctx;
  }

  public async Task<PaginateItem<T>> GetPaginatedEntityAsync(
    int pageNumber, CancellationToken cancellationToken
  )
  {
    return await _ctx.Set<T>()
                      .OrderBy(entity => EF.Property<object>(entity, "Id"))
                      .ToPagedResultAsync(pageNumber, cancellationToken);

  }
}

public interface IServiceHelper<T>
{
  Task<PaginateItem<T>> GetPaginatedEntityAsync(int pageNumber, CancellationToken cancellationToken);
}