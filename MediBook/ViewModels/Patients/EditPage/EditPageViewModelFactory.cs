using Microsoft.AspNetCore.Http;
using Platformus;
using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediBook.ViewModels.Patients
{
  public static class EditPageViewModelFactory
  {
    public static async Task<EditPageViewModel> CreateAsync(HttpContext httpContext, Patient patient)
    {
      return new EditPageViewModel()
      {
        FirstName = patient.FirstName,
        LastName = patient.LastName,
        MiddleName = patient.MiddleName,
        About = patient.About,
        Phone = patient.Phone
      };
    }

    public static EditPageViewModel Create(EditPageViewModel editPageViewModel)
    {
      return new EditPageViewModel
      { 
        FirstName = editPageViewModel.FirstName ?? string.Empty,
        LastName = editPageViewModel.LastName ?? string.Empty,
        MiddleName = editPageViewModel.MiddleName ?? string.Empty,
        About = editPageViewModel.About ?? string.Empty,
        Phone = editPageViewModel.Phone ?? string.Empty
      };
    }
  }
}