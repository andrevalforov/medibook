using Microsoft.AspNetCore.Authorization;
using Platformus.Core;

namespace MediBook
{
  public class HasBrowseContactTypesPermissionAuthorizationPolicyProvider : Platformus.Core.IAuthorizationPolicyProvider
  {
    public string Name => MediBook.Backend.Policies.HasBrowseContactTypesPermission;

    public AuthorizationPolicy GetAuthorizationPolicy()
    {
      AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

      authorizationPolicyBuilder.RequireAssertion(context =>
        {
          return context.User.HasClaim(PlatformusClaimTypes.Permission, MediBook.Backend.Permissions.BrowseContactTypes) || context.User.HasClaim(PlatformusClaimTypes.Permission, Platformus.Core.Permissions.DoAnything);
        }
      );

      return authorizationPolicyBuilder.Build();
    }
  }
}