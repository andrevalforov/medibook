using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus;
using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;
using MediBook.Data.Entities.Filters;

namespace MediBook.ViewModels.Doctors
{
  public static class BookPageViewModelFactory
  {
    public static BookPageViewModel Create(HttpContext httpContext, Doctor doctor, DateTime date)
    {
      return new BookPageViewModel()
      {
        Day = DayViewModelFactory.Create(httpContext, doctor, date),
        //Supervisions = httpContext.GetStorage().GetRepository<int, Consultation, ConsultationFilter>()
        //.GetAllAsync(new() { DoctorId = doctor.Id, Scheduled = new(from: date.Date, to: date.Date.AddDays(1))}).Result
        //.Select( s => ConsultationViewModelFactory.Create(httpContext, s).Result),
      };
    }
  }
}