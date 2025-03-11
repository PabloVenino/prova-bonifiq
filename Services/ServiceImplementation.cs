using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services;

public class ServiceImplementation : IServiceHelper
{

	public Task<PaginateItem<T>> ListItens<T>(int page)
	{
    

		return new Task<PaginateItem<T>>(null);
	}

}

public interface IServiceHelper
{
    Task<PaginateItem<T>> ListItens<T>(int page);
}