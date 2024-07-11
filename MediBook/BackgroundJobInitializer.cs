using Hangfire;
using Hangfire.Storage;
using System;
using MediBook.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MediBook
{
  public class BackgroundJobInitializer
  {
    public static void InitializeAsync(IServiceProvider serviceProvider)
    {
      using (var connection = JobStorage.Current.GetConnection())
        foreach (var recurringJob in connection.GetRecurringJobs())
          RecurringJob.RemoveIfExists(recurringJob.Id);

      //IEmailService emailService = serviceProvider.GetRequiredService<IEmailService>();

      //RecurringJob.AddOrUpdate(() => emailService.SendEmail(), Cron.Minutely);
    }
  }
}
