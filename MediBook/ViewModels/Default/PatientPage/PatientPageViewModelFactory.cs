using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus;
using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;

namespace MediBook.ViewModels.Default
{
  public static class PatientPageViewModelFactory
  {
    public static PatientPageViewModel Create(HttpContext httpContext, Patient patient, int currentUserId, bool withCalendar = false)
    {
      return new PatientPageViewModel()
      {
        Id = patient.Id,
        FullName = $"{patient.LastName} {patient.FirstName} {patient.MiddleName}",
        About = patient.About,
        Gender = patient.Gender,
        Email = patient.Email,
        Birthday = patient.Birthday,
        Phone = patient.Phone,
        CurrentUserId = currentUserId,
        Calendar = withCalendar
          ? CalendarViewModelFactory.Create(httpContext, patient, DateTime.Now.Year, DateTime.Now.Month)
          : null,
        Consultations = patient.Consultations?
          .Where(c => c.Status != ConsultationStatus.Canceled)
          .OrderByDescending(c => c.Created)
          .Select(c => Shared.ConsultationViewModelFactory.Create(httpContext, c).Result)
      };
    }
  }
}