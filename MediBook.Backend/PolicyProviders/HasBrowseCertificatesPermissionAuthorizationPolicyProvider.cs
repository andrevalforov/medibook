using Microsoft.AspNetCore.Authorization;
using Platformus.Core;

namespace MediBook
{
  public class HasBrowseCertificatesPermissionAuthorizationPolicyProvider : Platformus.Core.IAuthorizationPolicyProvider
  {
    public string Name => MediBook.Backend.Policies.HasBrowseCertificatesPermission;

    public AuthorizationPolicy GetAuthorizationPolicy()
    {
      AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

      authorizationPolicyBuilder.RequireAssertion(context =>
        {
          return context.User.HasClaim(PlatformusClaimTypes.Permission, MediBook.Backend.Permissions.BrowseCertificates) || context.User.HasClaim(PlatformusClaimTypes.Permission, Platformus.Core.Permissions.DoAnything);
        }
      );

      return authorizationPolicyBuilder.Build();
    }
  }
}