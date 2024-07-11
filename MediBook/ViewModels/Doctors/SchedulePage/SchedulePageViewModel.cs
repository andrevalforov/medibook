using System.Collections.Generic;
using MediBook.Attributes;
using MediBook.ViewModels.Shared;

namespace MediBook.ViewModels.Doctors
{
  public class SchedulePageViewModel
  {
    [MustBeTrue]
    public bool IsActivated { get; set; }

    public DayViewModel Day { get; set; }
    public IEnumerable<ConsultationViewModel> Consultations { get; set; }
  }
}