using System;

namespace MediBook.ViewModels.Shared
{
  public class DayViewModel
  {
    public string Url { get; set; }
    public DateTime Date { get; set; }
    public int ConsultationNumber { get; set; }
    public bool IsToday { get; set; }
  }
}