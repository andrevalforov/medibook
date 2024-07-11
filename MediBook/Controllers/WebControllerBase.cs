using Magicalizer.Data.Repositories.Abstractions;
using MediBook.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace MediBook.Controllers
{
  [Authorize(AuthenticationSchemes = FrontendCookieAuthenticationDefaults.AuthenticationScheme)]
  public class WebControllerBase : Controller
  {
    public IStorage Storage { get; }

    public WebControllerBase(IStorage storage)
    {
      this.Storage = storage;
    }

    protected RedirectResult CreateRedirectToSelfResult()
    {
      return this.Redirect(this.Request.Path.Value + this.Request.QueryString.Value);
    }

    protected int GetCurrentUserId()
    {
      if (!(this.User?.Identity?.IsAuthenticated ?? false))
        throw new HttpException(HttpStatusCode.Forbidden);

      if (!int.TryParse(this.User.Claims?.FirstOrDefault(c => string.Equals(c.Type, ClaimTypes.NameIdentifier))?.Value ?? string.Empty, out int id))
        throw new HttpException(HttpStatusCode.Forbidden);

      return id;
    }

    protected int? GetCurrentUserIdOrDefault()
    {
      if (!(this.User?.Identity?.IsAuthenticated ?? false))
        return null;

      if (!int.TryParse(this.User.Claims?.FirstOrDefault(c => string.Equals(c.Type, ClaimTypes.NameIdentifier))?.Value ?? string.Empty, out int id))
        return null;

      return id;
    }

    protected string GetCurrentUserRole()
    {
      if (!(this.User?.Identity?.IsAuthenticated ?? false))
        throw new HttpException(HttpStatusCode.Forbidden);

      return this.User.Claims?.FirstOrDefault(c => string.Equals(c.Type, ClaimTypes.Role))?.Value ?? string.Empty;
    }

    protected string GetCurrentUserRoleOrDefault() =>
      (this.User?.Identity?.IsAuthenticated ?? false)
        ? this.User.Claims?.FirstOrDefault(c => string.Equals(c.Type, ClaimTypes.Role))?.Value ?? string.Empty
        : string.Empty;
  }
}
