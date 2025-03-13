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
  private readonly List<IRule<T>> _rules;

  public ServiceImplementation(TestDbContext ctx, IEnumerable<IRule<T>> rules)
  {
    _ctx = ctx;
    _rules = rules.ToList();
  }

  public async Task<PaginateItem<T>> GetPaginatedEntityAsync(
    int pageNumber, CancellationToken cancellationToken
  )
  {
    return await _ctx.Set<T>()
                      .OrderBy(entity => EF.Property<object>(entity, "Id"))
                      .ToPagedResultAsync(pageNumber, cancellationToken);
  }

  public async Task<bool> IsValidAsync(T parameters)
  {
    foreach (var rule in _rules)
    {
      if (!await rule.IsSatisfiedAsync(parameters))
        return false;
    }
    return true;
  }
}