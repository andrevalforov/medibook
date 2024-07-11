using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace MediBook
{
  public static class IHttpRequestExtensions
  {
    public static void ParseIntQueryParameter(this HttpRequest requestHandler, string name, out int? result)
    {
      result = default;

      if (!requestHandler.HttpContext.Request.Query.TryGetValue(name, out StringValues value))
        return;

      if (int.TryParse(value, out int parsedId) && parsedId != default)
        result = parsedId;
    }
  }
}
