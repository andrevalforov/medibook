using System.ComponentModel.DataAnnotations;

namespace MediBook.ViewModels.Supervisees
{
  public class CompleteSupervisionPageViewModel
  {
    public int Id { get; set; }
    public string RedirectUrl { get; set; }
    public bool SuperviseeStatus { get; set; }

    [Display(Name = "Коментар")]
    [Required]
    [StringLength(1024)]
    public string SuperviseeComment { get; set; }

    [Display(Name = "Оцінка")]
    public int? SuperviseeScore { get; set; }

    [Display(Name = "Супервізія тривала менше 30 хв")]
    public bool SuperviseeLessThirtyMinutes { get; set; }
  }
}