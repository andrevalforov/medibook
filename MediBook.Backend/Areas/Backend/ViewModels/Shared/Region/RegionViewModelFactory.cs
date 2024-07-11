using Platformus;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Shared
{
  public static class RegionViewModelFactory
  {
    public static RegionViewModel Create(Region region)
    {
      return new RegionViewModel
      {
        Id = region.Id,
        Name = region.Name
      };
    }
  }
}
