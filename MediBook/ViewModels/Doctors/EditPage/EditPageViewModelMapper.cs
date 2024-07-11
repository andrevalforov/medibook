using Platformus;
using MediBook.Data.Entities;

namespace MediBook.ViewModels.Doctors
{
  public static class EditPageViewModelMapper
  {
    public static Doctor Map(EditPageViewModel editPageViewModel, Doctor doctor)
    {
      doctor.FullName = editPageViewModel.FullName;
      doctor.About = editPageViewModel.About ?? string.Empty;
      doctor.ProfessionalExperience = editPageViewModel.ProfessionalExperience ?? string.Empty;
      doctor.ProfessionalEducation = editPageViewModel.ProfessionalEducation ?? string.Empty;
      doctor.IsAvailableOnline = editPageViewModel.IsAvailableOnline;
      doctor.Room = editPageViewModel.Room;
      return doctor;
    }
  }
}