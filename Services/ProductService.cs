using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
  public class ProductService
  {
    private readonly TestDbContext _ctx;
    private readonly IServiceHelper _serviceHelper;

    public ProductService(TestDbContext ctx, IServiceHelper serviceHelper)
    {
      _ctx = ctx;
    }

    public ProductList ListProducts(int page)
    {
      _serviceHelper.ListItens<ProductList>(page);

      return new ProductList() { HasNext = false, TotalCount = 10, Products = _ctx.Products.ToList() };
    }
  }
}
