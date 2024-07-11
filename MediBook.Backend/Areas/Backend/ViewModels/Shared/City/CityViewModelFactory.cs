using Platformus;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Shared
{
  public static class CityViewModelFactory
  {
    public static CityViewModel Create(City city)
    {
      return new CityViewModel()
      {
        Id = city.Id,
        Name = city.Name,
        Region = city.Region.Name
      };
    }
  }
}
