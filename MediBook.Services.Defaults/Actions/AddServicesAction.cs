using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using MediBook.Services.Abstractions;
using System;

namespace MediBook.Services.Defaults.Actions
{
  public class AddServicesAction : IConfigureServicesAction
  {
    public int Priority => 1000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddScoped<IAuthService, AuthService>();
    }
  }
}
