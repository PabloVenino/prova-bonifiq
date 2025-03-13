
using ProvaPub.Enums;
using ProvaPub.Models;

namespace ProvaPub.Interfaces;

public interface IOrderService
{
  Task<Order> PayOrder(PaymentType paymentType, decimal paymentValue, int customerId);
}