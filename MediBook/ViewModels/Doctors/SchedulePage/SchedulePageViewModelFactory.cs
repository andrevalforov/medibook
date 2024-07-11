using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus;
using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;
using MediBook.Data.Entities.Filters;
using Magicalizer.Data.Repositories.Abstractions;

namespace MediBook.ViewModels.Doctors
{
  public static class SchedulePageViewModelFactory
  {
    public static async Task<SchedulePageViewModel> Create(HttpContext httpContext, Doctor doctor, DateTime date)
    {
      return new SchedulePageViewModel()
      {
        IsActivated = doctor.IsActivated,
        Day = DayViewModelFactory.Create(httpContext, doctor, date),
        Consultations = await GetConsultations(httpContext, doctor, date)
      };
    }

    private static async Task<IEnumerable<ConsultationViewModel>> GetConsultations(HttpContext httpContext, Doctor doctor, DateTime date)
    {
      List<ConsultationViewModel> consultations = new();
      IEnumerable<Consultation> existingConsultations = await httpContext.GetStorage().GetRepository<int, Consultation, ConsultationFilter>()
        .GetAllAsync(new() { DoctorId = doctor.Id, Scheduled = new(from: date.Date, to: date.Date.AddDays(1)) }, inclusions: new Inclusion<Consultation>(c => c.Patient));

      for (TimeOnly i = new(8, 0); i < new TimeOnly(21, 0); i = i.AddMinutes(30))
      {
        Consultation consultation = existingConsultations.FirstOrDefault(s => TimeOnly.FromTimeSpan(s.Scheduled.TimeOfDay) == i);

        if (consultation != null)
        {
          consultations.Add(await ConsultationViewModelFactory.Create(httpContext, consultation));
        }
        else
        {
          DateTime scheduled = new(date.Year, date.Month, date.Day, i.Hour, i.Minute, 0);
          consultations.Add(ConsultationViewModelFactory.Create(scheduled));
        }
      }

      return consultations;
    }
  }
}