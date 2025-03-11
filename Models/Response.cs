using System.Net;

namespace ProvaPub.Models
{
  public class Response<T>
  {
    public HttpStatusCode StatusCode { get; set; }
    public string? Message { get; set; }
    public Error? Error { get; set; } = null;
    public T? Data { get; set; }
  }

  public class Error
  {
    public string? ErrorCode { get; set; }
    public Exception? InnerException { get; set; } = null;
    public string? DeveloperMessage { get; set; }
  }
}