using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using MediBook.Services.Abstractions;

namespace MediBook.Actions
{
  public class AddAuthenticationAction : IConfigureServicesAction
  {
    public int Priority => 3010;

    public void Execute(IServiceCollection serviceCollection, IServiceProvider serviceProvider)
    {
      serviceCollection.AddAuthentication()
        .AddCookie(FrontendCookieAuthenticationDefaults.AuthenticationScheme, options =>
          {
            options.AccessDeniedPath = "/sign-in";
            options.LoginPath = "/sign-in";
            options.LogoutPath = "/sign-out";
            options.ReturnUrlParameter = "next";
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
          }
        );
    }
  }
}