using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediBook.ViewModels.Shared;

namespace MediBook.ViewModels.Doctors
{
  public class BookPageViewModel
  {
    [Required(ErrorMessage = "Оберіть значення зі списку")]
    public int? SupervisionId { get; set; }

    [Display(Name = "Примітки")]
    [StringLength(1024, ErrorMessage = "Довжина поля повинна бути не більше 1024 символів")]
    public string SuperviseeNotes { get; set; }

    public DayViewModel Day { get; set; }
    //public IEnumerable<SupervisionViewModel> Supervisions { get; set; }
  }
}