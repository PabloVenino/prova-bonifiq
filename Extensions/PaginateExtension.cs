using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;

namespace ProvaPub.Extensions
{
  public static class PaginateExtension
  {
    public static async Task<PaginateItem<T>> ToPagedResultAsync<T>(
      this IQueryable<T> query, int pageNumber, CancellationToken cancelationToken
    )
    {
      if (pageNumber <= 0)
        throw new ArgumentException("Page cannot be less than or equal to 0.");
      
      int pageSize = 10;

      int totalCount = await query.CountAsync(cancelationToken);
      List<T> items = await query.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync(cancelationToken);

      return new PaginateItem<T>
      {
        Item = items,
        HasNext = (int)Math.Ceiling((double)totalCount / pageSize) > pageNumber,
        TotalCount = totalCount
      };
    }
  }
}