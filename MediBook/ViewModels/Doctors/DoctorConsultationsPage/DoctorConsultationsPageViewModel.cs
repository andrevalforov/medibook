using System.Collections.Generic;
using MediBook.ViewModels.Shared;

namespace MediBook.ViewModels.Doctors
{
  public class DoctorConsultationsPageViewModel
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public bool IsAvailableIn24Hours { get; set; }
    public bool IsAvailableOnline { get; set; }
    public int ActionNeededCount { get; set; }

    public IEnumerable<ConsultationViewModel> ActiveConsultations { get; set; }
    public IEnumerable<ConsultationViewModel> ActionNeededConsultations { get; set; }
    public IEnumerable<ConsultationViewModel> ArchiveConsultations { get; set; }
    public CalendarViewModel Calendar { get; set; }
  }
}