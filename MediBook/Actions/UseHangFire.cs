using ExtCore.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using System;
using Hangfire;

namespace MediBook.Actions
{
  public class UseHangFire : IConfigureAction
  {
    public int Priority => 10000;

    public void Execute(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
    {
      BackgroundJobServerOptions options = new BackgroundJobServerOptions
      {
        SchedulePollingInterval = TimeSpan.FromSeconds(45),
        HeartbeatInterval = TimeSpan.FromMinutes(1)
      };

      applicationBuilder.UseHangfireServer(options);
      BackgroundJobInitializer.InitializeAsync(serviceProvider);
    }
  }
}
