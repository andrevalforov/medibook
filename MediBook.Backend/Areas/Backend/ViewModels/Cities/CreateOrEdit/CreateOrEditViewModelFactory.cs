using Microsoft.AspNetCore.Http;
using Platformus;
using Platformus.Core.Primitives;
using MediBook.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Magicalizer.Filters.Abstractions;

namespace MediBook.Backend.ViewModels.Cities
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(City city, HttpContext httpContext)
    {
      if (city is null)
        return new CreateOrEditViewModel
        {
          RegionValue = 0.ToString(),
          RegionOptions = GetRegionOptions(httpContext)
        };

      return new CreateOrEditViewModel
      {
        Id = city.Id,
        RegionValue = city.RegionId?.ToString() ?? 0.ToString(),
        RegionOptions = GetRegionOptions(httpContext)
      };
    }

    private static IEnumerable<Option> GetRegionOptions(HttpContext httpContext) =>
      httpContext.GetStorage().GetRepository<int, Region, IFilter>()
        .GetAllAsync().Result.Select(c => new Option(c.Name, c.Id.ToString()))
        .OrderBy(c => c.Text);
  }
}
