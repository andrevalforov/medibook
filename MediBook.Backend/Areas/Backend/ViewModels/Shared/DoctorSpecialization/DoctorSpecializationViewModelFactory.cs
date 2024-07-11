using System;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Shared
{
  public static class DoctorSpecializationViewModelFactory
  {
    public static DoctorSpecializationViewModel Create(Specialization specialization, DoctorSpecialization doctorSpecialization)
    {
      return new DoctorSpecializationViewModel
      {
        SpecializationId = specialization.Id,
        Name = specialization.Name,
        Selected = doctorSpecialization is not null
      };
    }
  }
}

