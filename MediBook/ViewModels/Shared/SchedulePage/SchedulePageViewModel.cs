using System.Collections.Generic;
using MediBook.Attributes;
using MediBook.ViewModels.Shared;

namespace MediBook.ViewModels.Shared
{
  public class SchedulePageViewModel
  {
    public int PatientId { get; set; }
    public string DoctorFullName { get; set; }
    public DayViewModel Day { get; set; }
    public IEnumerable<ConsultationViewModel> Consultations { get; set; }

    public int Id { get; set; }
    public bool IsOnline { get; set; }
    public string ReasonForAppeal { get; set; }
  }
}