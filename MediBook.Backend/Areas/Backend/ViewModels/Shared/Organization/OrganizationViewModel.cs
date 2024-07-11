using Platformus.Core.Backend.ViewModels;

namespace MediBook.Backend.ViewModels.Shared
{
  public class OrganizationViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string CityName { get; set; }
  }
}
