using MediBook.Backend.ViewModels.Shared;

using MediBook.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MediBook.Backend.ViewModels.Organizations
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(IEnumerable<Organization> organizations, string sorting, int offset, int limit, int total)
    {
      return new IndexViewModel
      {
        Organizations = organizations.Select(OrganizationViewModelFactory.Create),
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total
      };
    }
  }
}
