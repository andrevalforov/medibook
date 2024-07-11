using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Filters.Abstractions;
using MediBook.Backend.ViewModels.Shared;
using MediBook.Data.Entities;
using Microsoft.AspNetCore.Http;
using Platformus;
using Platformus.Core.Primitives;

namespace MediBook.Backend.ViewModels.Doctors
{
  public static class CreateOrEditViewModelFactory
  {
    public static async Task<CreateOrEditViewModel> CreateAsync(Doctor doctor, HttpContext httpContext)
    {
      IEnumerable<Specialization> specializations = await httpContext.GetStorage().GetRepository<int, Specialization, IFilter>().GetAllAsync(sorting: "+name");

      if (doctor is null)
        return new CreateOrEditViewModel
        {
          IsActivated = true,
          OrganizationOptions = await GetOrganizationOptions(httpContext),
          DoctorSpecializations = specializations.Select(sp => DoctorSpecializationViewModelFactory.Create(sp, null)).ToArray()
        };

      return new CreateOrEditViewModel
      {
        Id = doctor.Id,
        FullName = doctor.FullName,
        About = doctor.About,
        IsActivated = doctor.IsActivated,
        IsHidden = doctor.IsHidden,
        OrganizationId = doctor.OrganizationId,
        OrganizationOptions = await GetOrganizationOptions(httpContext),
        PhotoUrl = doctor.PhotoUrl,
        ProfessionalEducation = doctor.ProfessionalEducation,
        ProfessionalExperience = doctor.ProfessionalExperience,
        DoctorSpecializations = specializations.Select(sp => DoctorSpecializationViewModelFactory.Create(sp, doctor.Specializations.FirstOrDefault(_sp => _sp.SpecializationId == sp.Id))).ToArray(),
        Identifier = doctor.Email,
        Phone = doctor.Phone,
        IsAvailableOnline = doctor.IsAvailableOnline,
        Room = doctor.Room
      };
    }

    private static async Task<IEnumerable<Option>> GetOrganizationOptions(HttpContext httpContext) =>
      (await httpContext.GetStorage().GetRepository<int, Organization, IFilter>().GetAllAsync(sorting: "+name"))
      .Select(o => new Option(o.Name, o.Id.ToString()));

  }
}

