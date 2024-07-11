using Microsoft.AspNetCore.Http;
using Platformus;

using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Regions
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(Region region)
    {
      if (region is null)
        return new CreateOrEditViewModel();

      return new CreateOrEditViewModel
      {
        Id = region.Id,
        Name = region.Name
      };
    }
  }
}
