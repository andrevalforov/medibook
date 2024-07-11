using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus;
using MediBook.Data.Entities;
using MediBook.Data.Entities.Filters;
using Magicalizer.Data.Repositories.Abstractions;

namespace MediBook.ViewModels.Shared
{
  public static class SchedulePageViewModelFactory
  {
    public static async Task<SchedulePageViewModel> Create(HttpContext httpContext, Doctor doctor, DateTime date, int patientId)
    {
      return new SchedulePageViewModel()
      {
        PatientId = patientId,
        DoctorFullName = doctor.FullName,
        Day = DayViewModelFactory.Create(httpContext, doctor, date),
        Consultations = await GetConsultations(httpContext, doctor, date)
      };
    }

    private static async Task<IEnumerable<ConsultationViewModel>> GetConsultations(HttpContext httpContext, Doctor doctor, DateTime date)
    {
      if (date.Date < DateTime.Now.Date) return null;

      IEnumerable<ConsultationViewModel> consultations = (await httpContext.GetStorage().GetRepository<int, Consultation, ConsultationFilter>()
        .GetAllAsync(new() { DoctorId = doctor.Id, Scheduled = new(from: date.Date, to: date.Date.AddDays(1)) }, sorting: "+scheduled", inclusions: new Inclusion<Consultation>[]
        {
          new Inclusion<Consultation>(c => c.Doctor),
          new Inclusion<Consultation>(c => c.Patient)
        }))
        .Where(c => c.Status != ConsultationStatus.Canceled)
        .Select(c => ConsultationViewModelFactory.Create(httpContext, c).Result);

      return consultations;
    }
  }
}