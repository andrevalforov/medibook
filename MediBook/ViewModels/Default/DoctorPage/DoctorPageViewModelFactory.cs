using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;
using Platformus;
using MediBook.Data.Entities.Filters;

namespace MediBook.ViewModels.Default
{
  public static class DoctorPageViewModelFactory
  {
    public static DoctorPageViewModel Create(HttpContext httpContext, Doctor doctor, bool withCalendar = false)
    {
      return new DoctorPageViewModel()
      {
        Id = doctor.Id,
        FullName = doctor.FullName,
        About = doctor.About,
        ProfessionalEducation = doctor.ProfessionalEducation,
        ProfessionalExperience = doctor.ProfessionalExperience,
        Specializations = doctor.Specializations?.Select(sp => SpecializationViewModelFactory.Create(sp.Specialization)),
        Organization = doctor.Organization?.Name,
        City = doctor.Organization?.City?.Name,
        Region = doctor.Organization?.City?.Region?.Name,
        IsAvailableOnline = doctor.IsAvailableOnline,
        Room = doctor.Room,
        IsAvailableIn24Hours = doctor.Consultations.Count > 0,
        HasAnyAvailableConsultation = httpContext.GetStorage().GetRepository<int, Consultation, ConsultationFilter>()
          .GetAllAsync(new() { DoctorId = doctor.Id, Scheduled = new(from: DateTime.Now.AddMinutes(30)), Status = new(equals: (int)ConsultationStatus.Available) }, limit: 1)
          .Result.Any(),
        PhotoUrl = doctor.PhotoUrl ?? string.Empty,
        Consultations = doctor.Consultations
          .Select(s => Shared.ConsultationViewModelFactory.Create(httpContext, s).Result),
        Calendar = withCalendar ? CalendarViewModelFactory.Create(httpContext, doctor, DateTime.Now.Year, DateTime.Now.Month) : null
      };
    }
  }
}