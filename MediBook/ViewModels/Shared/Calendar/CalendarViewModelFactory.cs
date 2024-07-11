using System;
using Microsoft.AspNetCore.Http;
using MediBook.Data.Entities;

namespace MediBook.ViewModels.Shared
{
  public static class CalendarViewModelFactory
  {
    public static CalendarViewModel Create(HttpContext httpContext, Doctor doctor, int year, int month)
    {
      DayViewModel[,] daysByWeeks = new DayViewModel[6, 7];
      DateTime date = new DateTime(year, month, 1);

      date = date.AddDays(((int)date.DayOfWeek - 1) * -1);
      DateTime firstDate = date;

      for (int i = 0; i < 6; i++)
      {
        for (int j = 0; j < 7; j++)
        {
          daysByWeeks[i, j] = DayViewModelFactory.Create(httpContext, doctor, date);
          date = date.AddDays(1);
        }
      }

      return new CalendarViewModel()
      {
        DaysByWeeks = daysByWeeks,
        FirstDate = firstDate
      };
    }

    public static CalendarViewModel Create(HttpContext httpContext, Patient patient, int year, int month)
    {
      DayViewModel[,] daysByWeeks = new DayViewModel[6, 7];
      DateTime date = new DateTime(year, month, 1);

      date = date.AddDays(((int)date.DayOfWeek - 1) * -1);
      DateTime firstDate = date;

      for (int i = 0; i < 6; i++)
      {
        for (int j = 0; j < 7; j++)
        {
          daysByWeeks[i, j] = DayViewModelFactory.Create(httpContext, patient, date);
          date = date.AddDays(1);
        }
      }

      return new CalendarViewModel()
      {
        DaysByWeeks = daysByWeeks,
        FirstDate = firstDate
      };
    }
  }
}