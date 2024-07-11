using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MediBook
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      try
      {
        await _next(httpContext);
      }
      catch (Exception ex)
      {
        this.HandleException(httpContext, ex);
      }
    }

    private void HandleException(HttpContext context, Exception exception)
    {
      context.Response.Redirect($"/errors/{GetStatusCode(exception)}.html");
    }

    private static int GetStatusCode(Exception exception)
    {
      if (exception is InvalidOperationException)
        return (int)HttpStatusCode.Unauthorized;

      return (int)HttpStatusCode.InternalServerError;
    }
  }
}
