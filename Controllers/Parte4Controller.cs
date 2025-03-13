using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{

  /// <summary>
  /// O Código abaixo faz uma chmada para a regra de negócio que valida se um consumidor pode fazer uma compra.
  /// Crie o teste unitário para esse Service. Se necessário, faça as alterações no código para que seja possível realizar os testes.
  /// Tente criar a maior cobertura possível nos testes.
  /// 
  /// Utilize o framework de testes que desejar. 
  /// Crie o teste na pasta "Tests" da solution
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class Parte4Controller : ControllerBase
  {
    private readonly ICustomerService _customerService;
    public Parte4Controller(ICustomerService customerService)
    {
      _customerService = customerService;
    }

    [HttpGet("can-purchase")]
    public async Task<Response<object>> CanPurchase(int customerId, decimal purchaseValue)
    {
      var canPurchase = await _customerService.CanPurchase(customerId, purchaseValue);
      var response = new Response<object>
      {
        Data = new { CanPurchase = canPurchase },
        Message = canPurchase ? "Você ainda pode efetuar a compra desse mês" : "Você já comprou esse mês, não?",
        StatusCode = HttpStatusCode.OK
      };
      
      return response;
    }
  }
}
