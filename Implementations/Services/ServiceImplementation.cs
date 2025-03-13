using System.Threading;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Extensions;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Implementations;

public class ServiceImplementation<T> : IServiceAbstractions<T> where T : class
{
  private readonly TestDbContext _ctx;

  public ServiceImplementation(TestDbContext ctx)
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