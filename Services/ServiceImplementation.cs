using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services;

public class ServiceImplementation : IServiceHelper
{

	public List<T> ListItens<T>(int page)
	{


		return new List<T>();
	}

}

public interface IServiceHelper
{
    List<T> ListItens<T>(int page);
}