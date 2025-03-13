
using ProvaPub.Models;

namespace ProvaPub.Interfaces;

public interface IServiceAbstractions<T>
{
  Task<PaginateItem<T>> GetPaginatedEntityAsync(int pageNumber, CancellationToken cancellationToken);
  Task<bool> IsValidAsync(T parameters);
}