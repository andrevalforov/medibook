using System;
using System.Linq;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Shared
{
  public static class DoctorViewModelFactory
  {
    public static DoctorViewModel Create(Doctor doctor)
    {
      return new DoctorViewModel
      {
        Id = doctor.Id,
        FullName = doctor.FullName,
        Organization = doctor.Organization.Name,
        Specializations = string.Join(", ", doctor.Specializations.Select(sp => sp.Specialization.Name)),
        Created = doctor.Created.ToString("dd.MM.yyyy")
      };

    }
  }
}

