using ProvaPub.Interfaces;

namespace ProvaPub.Implementations;

public class PayPalPayment : IPayment
{
  public void ProcessPayment()
  {
    Console.WriteLine("Processando Cartão de Crédito.");
  }
}