using ExtCore.Infrastructure.Actions;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MediBook.Actions
{
  public class AddHangFire : IConfigureServicesAction
  {
    public int Priority => 10000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      IConfiguration configuration = serviceProvider.GetService<IConfiguration>();
      services.AddHangfire(x => x
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(configuration.GetConnectionString("Default"), new SqlServerStorageOptions
        {
          CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
          SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
          QueuePollInterval = TimeSpan.FromMinutes(5),
          UseRecommendedIsolationLevel = true,
          UsePageLocksOnDequeue = true,
          DisableGlobalLocks = true
        }));
    }
  }
}
