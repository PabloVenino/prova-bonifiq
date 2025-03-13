using ProvaPub.Interfaces;

namespace ProvaPub.Implementations;

public class CreditCardPayment : IPayment
{
  public void ProcessPayment()
  {
    Console.WriteLine("Processando Cartão de Crédito.");
  }
}