using System;
using System.Linq;
using MediBook.Data.Entities;

namespace MediBook.ViewModels.Shared
{
  public static class DoctorViewModelFactory
  {
    public static DoctorViewModel Create(Doctor doctor, bool? isAvailableIn24Hours = null)
    {
      return new DoctorViewModel
      {
        Id = doctor.Id,
        FullName = doctor.FullName,
        Organization = doctor.Organization is not null ? OrganizationViewModelFactory.Create(doctor.Organization) : null,
        PhotoUrl = doctor.PhotoUrl,
        City = doctor.Organization?.City?.Name,
        Specializations = doctor.Specializations?.Select(sp => SpecializationViewModelFactory.Create(sp.Specialization)),
        About = doctor.About,
        Room = doctor.Room,
        IsAvailableIn24Hours = isAvailableIn24Hours ?? false,
        IsAvailableOnline = doctor.IsAvailableOnline
      };

    }
  }
}

