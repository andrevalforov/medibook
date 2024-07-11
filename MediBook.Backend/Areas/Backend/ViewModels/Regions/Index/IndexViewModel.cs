
using System.Collections.Generic;
using MediBook.Backend.ViewModels.Shared;

namespace MediBook.Backend.ViewModels.Regions
{
  public class IndexViewModel : IndexViewModelBase
  {
    public IEnumerable<RegionViewModel> Regions { get; set; }
  }
}
