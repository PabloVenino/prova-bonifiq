using static System.Net.HttpStatusCode;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Net;

namespace ProvaPub.Handlers
{
  public class GlobalExceptionMiddleware
  {
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
      try
      {
        await _next(httpContext);
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(httpContext, ex);
      }
    }

    public async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
      var response = ExceptionHandlers.TryGetValue(ex.GetType(), out var handler)
        ? handler(ex)
        : CreateErrorResponse<int>("INTERNAL_SERVER_ERROR", ex, InternalServerError, "Um erro inesperado ocorreu, tente novamente mais tarde");

      httpContext.Response.ContentType = "application/json";
      httpContext.Response.StatusCode = (int)response.StatusCode;

      await httpContext.Response.WriteAsJsonAsync(response);
    }

    private static readonly Dictionary<Type, Func<Exception, Response<int>>> ExceptionHandlers = new()
    {
      {
        typeof(ArgumentException),
        ex => CreateErrorResponse<int>("ARGUMENT_EXCEPTION", ex, BadRequest, "Um valor passado está incorreto, tente novamente.")
      },
      {
        typeof(TimeoutException),
        ex => CreateErrorResponse<int>("TIMEOUT_EXCEPTION", ex, RequestTimeout, "Timeout: o processamento demorou mais que o esperado.")
      },
      {
        typeof(DbUpdateException), ex => HandleDbUpdate<int>(ex)
      }
    };

    private static Response<T> CreateErrorResponse<T>(string code, Exception ex, HttpStatusCode statusCode, string message)
    {
      return new Response<T>
      {
        Error = new Error
        {
          ErrorCode = code,
          InnerException = ex,
          DeveloperMessage = $"Algo não saiu como o esperado: {ex}"
        },
        StatusCode = statusCode,
        Message = message
      };
    }

    private static Response<T> HandleDbUpdate<T>(Exception ex)
    {
      var sqlEx = ex.InnerException as SqlException;
      var errorCode = sqlEx?.Number switch
      {
        2601 or 2627 => "UNIQUE_CONSTRAINT_VIOLATION",
        _ => "UPDATE_EXCEPTION"
      };

      return CreateErrorResponse<T>(errorCode, ex, Conflict, "Dado a ser inserido no banco não pode ser repetido");
    }
  }
}