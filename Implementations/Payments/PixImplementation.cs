using ProvaPub.Interfaces;

namespace ProvaPub.Implementations;

public class PixPayment : IPayment
{
  public void ProcessPayment()
  {
    Console.WriteLine("Processando Cartão de Crédito.");
  }
}