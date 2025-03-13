using ProvaPub.Enums;
using ProvaPub.Implementations;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services;

public class OrderService : IOrderService
{
  private TestDbContext _ctx;

  private readonly Dictionary<PaymentType, IPayment> _payment = new()
    {
      { PaymentType.PIX, new PixPayment() },
      { PaymentType.CREDIT_CARD, new CreditCardPayment() },
      { PaymentType.PAYPAL, new PayPalPayment() },
      // Adicionar outros metodos de pagamento aqui
    };

  public OrderService(TestDbContext ctx)
  {
    _ctx = ctx;
  }


  public async Task<Order> PayOrder(PaymentType paymentType, decimal paymentValue, int customerId)
  {
    if (_payment.TryGetValue(paymentType, out var type))
    {
      type.ProcessPayment();
    }
    else
    {
      throw new ArgumentException("PaymentType desconhecido");
    }

    return await InsertOrder(new Order()
    {
      CustomerId = customerId,
      Value = paymentValue,
      OrderDate = DateTime.UtcNow
    });
  }

  private async Task<Order> InsertOrder(Order order)
  {
    var entity = (await _ctx.Orders.AddAsync(order)).Entity;
    entity.OrderDate = entity.OrderDate.ToLocalTime();
    return entity;
  }
}
