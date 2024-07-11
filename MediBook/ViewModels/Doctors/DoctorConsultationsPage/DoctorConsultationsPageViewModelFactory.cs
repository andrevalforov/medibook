using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;

namespace MediBook.ViewModels.Doctors
{
  public static class DoctorConsultationsPageViewModelFactory
  {
    public static DoctorConsultationsPageViewModel Create(HttpContext httpContext, Doctor doctor)
    {
      return new DoctorConsultationsPageViewModel()
      {
        Id = doctor.Id,
        FullName = doctor.FullName,
        IsAvailableOnline = doctor.IsAvailableOnline,
        IsAvailableIn24Hours = doctor.Consultations.Count > 0,
        ActiveConsultations = doctor.Consultations.Where(c => c.Status == ConsultationStatus.Booked && c.Scheduled > DateTime.Now.AddMinutes(-30))
          .Select(s => ConsultationViewModelFactory.Create(httpContext, s).Result)
          .OrderBy(c => c.Scheduled),
        ActionNeededConsultations = doctor.Consultations.Where(c => c.Status == ConsultationStatus.Booked && c.Scheduled <= DateTime.Now.AddMinutes(-30))
          .Select(s => ConsultationViewModelFactory.Create(httpContext, s).Result)
          .OrderBy(c => c.Scheduled),
        ArchiveConsultations = doctor.Consultations.Where(c => c.Status == ConsultationStatus.Canceled || c.Status == ConsultationStatus.Completed)
          .Select(s => ConsultationViewModelFactory.Create(httpContext, s).Result)
          .OrderByDescending(c => c.Scheduled),
        Calendar = CalendarViewModelFactory.Create(httpContext, doctor, DateTime.Now.Year, DateTime.Now.Month),
        ActionNeededCount = doctor.Consultations.Count(c => c.Status == ConsultationStatus.Booked && c.Scheduled <= DateTime.Now.AddMinutes(-30))
      };
    }
  }
}