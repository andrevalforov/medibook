using Platformus;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Shared
{
  public static class OrganizationViewModelFactory
  {
    public static OrganizationViewModel Create(Organization organization)
    {
      return new OrganizationViewModel()
      {
        Id = organization.Id,
        Name = organization.Name,
        CityName = organization.City.Name
      };
    }
  }
}
