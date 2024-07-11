using MediBook.Data.Entities;

namespace MediBook.ViewModels.Shared
{
  public static class OrganizationViewModelFactory
  {
    public static OrganizationViewModel Create(Organization organization)
    {
      return new OrganizationViewModel
      {
        Id = organization.Id,
        Name = organization.Name,
        Address = organization.Address,
        City = organization.City?.Name,
        Region = organization.City?.Region?.Name
      };
    }
  }
}
