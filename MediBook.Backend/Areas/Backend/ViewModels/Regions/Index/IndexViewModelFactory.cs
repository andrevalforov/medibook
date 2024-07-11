using MediBook.Backend.ViewModels.Shared;

using MediBook.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MediBook.Backend.ViewModels.Regions
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(IEnumerable<Region> regions, string sorting, int offset, int limit, int total)
    {
      return new IndexViewModel
      {
        Regions = regions.Select(RegionViewModelFactory.Create),
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total
      };
    }
  }
}
