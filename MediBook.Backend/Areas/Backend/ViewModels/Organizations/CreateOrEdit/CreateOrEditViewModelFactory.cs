using Microsoft.AspNetCore.Http;
using Platformus;
using Platformus.Core.Primitives;

using MediBook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using MediBook.Data.Entities.Filters;
using System.Threading.Tasks;
using Magicalizer.Filters.Abstractions;

namespace MediBook.Backend.ViewModels.Organizations
{
  public static class CreateOrEditViewModelFactory
  {
    public static async Task<CreateOrEditViewModel> CreateAsync(Organization organization, HttpContext httpContext)
    {
      if (organization is null)
        return new CreateOrEditViewModel
        {
          CityValue = 0.ToString(),
          CityOptions = await GetCityOptions(httpContext, null),
          RegionOptions = await GetRegionOptions(httpContext)
        };

      return new CreateOrEditViewModel
      {
        Id = organization.Id,
        Name = organization.Name,
        CityValue = organization.City.Id.ToString(),
        RegionValue = organization.City.RegionId.ToString(),
        Address = organization.Address,
        CityOptions = await GetCityOptions(httpContext, organization.City.RegionId),
        RegionOptions = await GetRegionOptions(httpContext)
      };
    }

    private static async Task<IEnumerable<Option>> GetCityOptions(HttpContext httpContext, int? regionId) =>
     (await httpContext.GetStorage().GetRepository<int, City, CityFilter>().GetAllAsync(new() { RegionId = regionId }))
       .Select(c => new Option(c.Name, c.Id.ToString()))
       .OrderBy(c => c.Text);

    private static async Task<IEnumerable<Option>> GetRegionOptions(HttpContext httpContext) =>
      (await httpContext.GetStorage().GetRepository<int, Region, IFilter>().GetAllAsync())
        .Select(c => new Option(c.Name, c.Id.ToString()))
        .OrderBy(c => c.Text);
  }
}
