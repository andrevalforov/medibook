using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Core.Backend;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;

namespace MediBook.Backend.ViewModels.Organizations
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }

    [Display(Name = "Назва")]
    [Required]
    [StringLength(64)]
    public string Name { get; set; }

    [Display(Name = "Місто")]
    [Required]
    public string CityValue { get; set; }
    public IEnumerable<Option> CityOptions { get; set; }

    [Display(Name = "Область")]
    [Required]
    public string RegionValue { get; set; }
    public IEnumerable<Option> RegionOptions { get; set; }

    [Display(Name = "Адреса")]
    [Required]
    [StringLength(256)]
    public string Address { get; set; }
  }
}
