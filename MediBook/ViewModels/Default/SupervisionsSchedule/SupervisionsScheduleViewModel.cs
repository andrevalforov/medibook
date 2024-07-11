using System;
using System.Collections.Generic;

namespace MediBook.ViewModels.Shared
{
  public class ConsultationsScheduleViewModel
  {
    public bool IsCurrent { get; set; }
    public int CanBeCanceledPeriod { get; set; }
    public DateTime Date { get; set; }
    public IEnumerable<ConsultationViewModel> Consultations { get; set; }
  }
}
