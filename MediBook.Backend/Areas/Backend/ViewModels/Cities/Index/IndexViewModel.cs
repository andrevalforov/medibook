using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Core.Primitives;
using MediBook.Backend.ViewModels.Shared;

namespace MediBook.Backend.ViewModels.Cities
{
  public class IndexViewModel : IndexViewModelBase
  {
    public IEnumerable<CityViewModel> Cities { get; set; }

    [Display(Name = "Область")]
    public string Region { get; set; }
    public IEnumerable<Option> RegionOptions { get; set; }
  }
}
