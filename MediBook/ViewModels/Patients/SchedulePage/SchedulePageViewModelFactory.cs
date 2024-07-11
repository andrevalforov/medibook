using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus;
using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;
using MediBook.Data.Entities.Filters;

namespace MediBook.ViewModels.Supervisees
{
  public static class SchedulePageViewModelFactory
  {
    public static async Task<SchedulePageViewModel> Create(HttpContext httpContext, Patient patient, DateTime date)
    {
      return new SchedulePageViewModel()
      {
        Day = DayViewModelFactory.Create(httpContext, patient, date),
        //Supervisions = (await httpContext.GetStorage().GetRepository<int, Consultation, ConsultationFilter>()
        //  .GetAllAsync(new() { PatientId = patient.Id, Scheduled = new(from: date.Date, to: date.Date.AddDays(1))}))
        //  .Select(s => ConsultationViewModelFactory.Create(httpContext, s).Result)
      };
    }
  }
}