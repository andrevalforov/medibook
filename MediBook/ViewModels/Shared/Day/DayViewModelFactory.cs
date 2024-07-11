using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Platformus;
using MediBook.Data.Entities;
using MediBook.Services.Abstractions;
using MediBook.Data.Entities.Filters;
using System.Numerics;

namespace MediBook.ViewModels.Shared
{
  public static class DayViewModelFactory
  {
    public static DayViewModel Create(HttpContext httpContext, Doctor doctor, DateTime date)
    {
      ClaimsPrincipal principal = httpContext.AuthenticateAsync("FrontendCookies").Result?.Principal;
      int userId = int.Parse(principal?.Claims?.FirstOrDefault(cl => cl.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

      string url = null;
      int consultationsCount = doctor is null
        ? default
        : httpContext.GetStorage().GetRepository<int, Consultation, ConsultationFilter>()
          .CountAsync(new()
          {
            DoctorId = doctor.Id,
            Scheduled = new(from: date.Date, to: date.Date.AddDays(1)),
            Status = (principal?.IsInRole("Doctor") ?? false) ? null : new(equals: (int)ConsultationStatus.Available)
          }).Result;

      if (principal?.IsInRole("Doctor") ?? false)
      {
        if (doctor.Id == userId)
          url = $"/doctors/me/schedule/{date.Year}/{date.Month}/{date.Day}";
      }
      else if (consultationsCount > 0 && date.Date >= DateTime.Today)
      {
        if (principal?.IsInRole("Patient") ?? false)
          url = $"/doctors/{doctor.Id}/book/{date.Year}/{date.Month}/{date.Day}";

        else url = $"/sign-in";
      }

      return new DayViewModel()
      {
        Url = url,
        Date = date,
        ConsultationNumber = consultationsCount,
        IsToday = date.Date == DateTime.Today
      };
    }

    public static DayViewModel Create(HttpContext httpContext, Patient patient, DateTime date)
    {
      ClaimsPrincipal principal = httpContext.AuthenticateAsync("FrontendCookies").Result?.Principal;
      int userId = int.Parse(principal?.Claims?.FirstOrDefault(cl => cl.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

      string url;
      int consultationsCount = patient is null
        ? default
        : httpContext.GetStorage().GetRepository<int, Consultation, ConsultationFilter>()
          .CountAsync(new() { PatientId = patient.Id, Scheduled = new(from: date.Date, to: date.Date.AddDays(1)) }).Result;

      if (patient == null)
        url = $"/sign-in";

      else if (principal?.IsInRole("Patient") ?? false && patient.Id == userId && consultationsCount > 0)
        url = $"/patients/me/schedule/{date.Year}/{date.Month}/{date.Day}";

      else url = null;

      return new DayViewModel()
      {
        Url = url,
        Date = date,
        ConsultationNumber = consultationsCount,
        IsToday = date.Date == DateTime.Today
      };
    }
  }
}