using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Filters.Abstractions;
using MediBook.Data.Entities;
using MediBook.Data.Entities.Filters;
using MediBook.ViewModels.Shared;
using Microsoft.AspNetCore.Http;
using Platformus;
using Platformus.Core.Primitives;

namespace MediBook.ViewModels.Doctors
{
  public static class DoctorsPageViewModelFactory
  {
    public static async Task<DoctorsPageViewModel> Create(IEnumerable<Doctor> doctors, HttpContext httpContext)
    {
      return new DoctorsPageViewModel
      {
        Doctors = doctors.Select(d => DoctorViewModelFactory.Create(d, IsAvailableIn24Hours(d))),
        RegionOptions = await GetRegionOptions(httpContext),
        SpecializationOptions = await GetSpecializationOptions(httpContext),
        IsOnlineOptions = GetIsOnlineOptions()
      };
    }

    private static async Task<IEnumerable<Option>> GetRegionOptions(HttpContext httpContext) =>
      (await httpContext.GetStorage().GetRepository<int, Region, IFilter>().GetAllAsync(sorting: "+name"))
      .Select(r => new Option(r.Name, r.Id.ToString()))
      .Prepend(new Option("Будь-яка область", string.Empty));

    private static async Task<IEnumerable<Option>> GetSpecializationOptions(HttpContext httpContext) =>
      (await httpContext.GetStorage().GetRepository<int, Specialization, IFilter>().GetAllAsync(sorting: "+name"))
      .Select(sp => new Option(sp.Name, sp.Id.ToString()))
      .Prepend(new Option("Усі спеціальності", string.Empty));

    private static IEnumerable<Option> GetIsOnlineOptions() =>
      new List<Option>
      {
        new("Онлайн та офлайн запис", string.Empty),
        new("Приймають онлайн", 1.ToString())
      };

    private static bool IsAvailableIn24Hours(Doctor doctor) => doctor.Consultations.Count > 0;

  }
}

