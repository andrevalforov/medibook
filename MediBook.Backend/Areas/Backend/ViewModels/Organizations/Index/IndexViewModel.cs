
using System.Collections.Generic;
using MediBook.Backend.ViewModels.Shared;

namespace MediBook.Backend.ViewModels.Organizations
{
  public class IndexViewModel : IndexViewModelBase
  {
    public IEnumerable<OrganizationViewModel> Organizations { get; set; }
  }
}
