using Microsoft.AspNetCore.Authorization;
using Platformus.Core;

namespace MediBook
{
  public class HasBrowseEmailsPermissionAuthorizationPolicyProvider : Platformus.Core.IAuthorizationPolicyProvider
  {
    public string Name => MediBook.Backend.Policies.HasBrowseEmailsPermission;

    public AuthorizationPolicy GetAuthorizationPolicy()
    {
      AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

      authorizationPolicyBuilder.RequireAssertion(context =>
        {
          return context.User.HasClaim(PlatformusClaimTypes.Permission, MediBook.Backend.Permissions.BrowseEmails) || context.User.HasClaim(PlatformusClaimTypes.Permission, Platformus.Core.Permissions.DoAnything);
        }
      );

      return authorizationPolicyBuilder.Build();
    }
  }
}