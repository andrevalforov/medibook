using System;

namespace MediBook.ViewModels.Shared
{
  public class CalendarViewModel
  {
    public DayViewModel[,] DaysByWeeks { get; set; }
    public DateTime FirstDate { get; set; }
  }
}