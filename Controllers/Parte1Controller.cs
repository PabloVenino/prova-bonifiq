using Microsoft.AspNetCore.Mvc;
using ProvaPub.Services;
using ProvaPub.Models;
using ProvaPub.Handlers;

namespace ProvaPub.Controllers
{
  /// <summary>
  /// Ao rodar o código abaixo o serviço deveria sempre retornar um número diferente, mas ele fica retornando sempre o mesmo número.
  /// 1 - Faça as alterações para que o retorno seja sempre diferente
  /// 2 - Tome cuidado 
  /// 3 - Oi, tomei cuidado!
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class Parte1Controller : ControllerBase
  {
    private readonly RandomService _randomService;

    public Parte1Controller(RandomService randomService)
    {
      _randomService = randomService;
    }

    [HttpGet]
    public async Task<ActionResult<int>> Index()
    {
      int number = await _randomService.GetRandom();

      return Ok(new Response<int>
      {
        Data = number,
        Message = "Número retornado com sucesso."
      });

    }
  }
}
