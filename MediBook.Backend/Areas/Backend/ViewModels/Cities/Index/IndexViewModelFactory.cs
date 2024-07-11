using Microsoft.AspNetCore.Http;
using Platformus;
using Platformus.Core.Primitives;
using MediBook.Backend.ViewModels.Shared;

using MediBook.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Magicalizer.Filters.Abstractions;

namespace MediBook.Backend.ViewModels.Cities
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(IEnumerable<City> cities, HttpContext httpContext, string sorting, int offset, int limit, int total)
    {
      return new IndexViewModel()
      {
        Cities = cities.Select(CityViewModelFactory.Create),
        RegionOptions = GetRegionOptions(httpContext),
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total
      };
    }

    private static IEnumerable<Option> GetRegionOptions(HttpContext httpContext) =>
      httpContext.GetStorage().GetRepository<int, Region, IFilter>()
        .GetAllAsync().Result.Select(r => new Option(r.Name, r.Id.ToString()))
        .OrderBy(o => o.Text)
        .Prepend(new Option("Усі області", string.Empty));
  }
}
