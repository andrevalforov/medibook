using System.Collections.Generic;
using MediBook.ViewModels.Shared;

namespace MediBook.ViewModels.Default
{
  public class DoctorPageViewModel
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Organization { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public IEnumerable<SpecializationViewModel> Specializations { get; set; }
    public string PhotoUrl { get; set; }
    public string About { get; set; }
    public string Room { get; set; }
    public string ProfessionalExperience { get; set; }
    public string ProfessionalEducation { get; set; }
    public bool IsAvailableIn24Hours { get; set; }
    public bool IsAvailableOnline { get; set; }
    public bool HasAnyAvailableConsultation { get; set; }

    public IEnumerable<Shared.ConsultationViewModel> Consultations { get; set; }
    public CalendarViewModel Calendar { get; set; }
  }
}