using Microsoft.AspNetCore.Http;
using Platformus;
using MediBook.Data.Entities;

namespace MediBook.ViewModels.Patients
{
  public static class EditPageViewModelMapper
  {
    public static Patient Map(EditPageViewModel editPageViewModel, Patient patient)
    {
      patient.FirstName = editPageViewModel.FirstName;
      patient.LastName = editPageViewModel.LastName;
      patient.MiddleName = editPageViewModel.MiddleName ?? string.Empty;
      patient.About = editPageViewModel.About;
      patient.Phone = editPageViewModel.Phone;
      return patient;
    }
  }
}