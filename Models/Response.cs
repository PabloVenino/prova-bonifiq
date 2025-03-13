using System.Net;

namespace ProvaPub.Models
{
  public class Response<T>
  {
    public HttpStatusCode StatusCode { get; set; }
    public string? Message { get; set; }
    public ErrorDetail? Error { get; set; } = null;
    public T? Data { get; set; }
  }

  public class ErrorDetail
  {
    public string? ErrorCode { get; set; }
    public string? InnerException { get; set; }
    public string? StackTrace { get; set; }
    public string? DeveloperMessage { get; set; }
  }
}