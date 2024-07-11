using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus;
using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;

namespace MediBook.ViewModels.Doctors
{
  public static class EditPageViewModelFactory
  {
    public static EditPageViewModel Create(HttpContext httpContext, Doctor doctor)
    {
      return new EditPageViewModel
      {
        FullName = doctor.FullName,
        About = doctor.About ?? string.Empty,
        ProfessionalExperience = doctor.ProfessionalExperience ?? string.Empty,
        ProfessionalEducation = doctor.ProfessionalEducation ?? string.Empty,
        IsAvailableOnline = doctor.IsAvailableOnline,
        Room = doctor.Room
      };
    }

    public static EditPageViewModel Create(EditPageViewModel editPageViewModel)
    {
      return new EditPageViewModel
      {
        FullName = editPageViewModel.FullName ?? string.Empty,
        About = editPageViewModel.About ?? string.Empty,
        ProfessionalExperience = editPageViewModel?.ProfessionalExperience ?? string.Empty,
        ProfessionalEducation = editPageViewModel?.ProfessionalEducation ?? string.Empty,
        Room = editPageViewModel.Room ?? string.Empty,
        IsAvailableOnline = editPageViewModel.IsAvailableOnline
      };
    }
  }
}