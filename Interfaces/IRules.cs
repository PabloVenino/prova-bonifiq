namespace ProvaPub.Interfaces;

public interface IRule<T>
{
  Task<bool> IsSatisfiedAsync(T parameters);
}
