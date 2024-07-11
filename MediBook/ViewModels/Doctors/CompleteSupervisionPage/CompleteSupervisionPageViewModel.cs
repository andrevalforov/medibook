using System.ComponentModel.DataAnnotations;

namespace MediBook.ViewModels.Doctors
{
  public class CompleteSupervisionPageViewModel
  {
    public int Id { get; set; }
    public string RedirectUrl { get; set; }
    public bool SupervisorStatus { get; set; }

    [Display(Name = "Коментар супервізанта")]
    [Required]
    [StringLength(1024)]
    public string SupervisorComment { get; set; }

    [Display(Name = "Встановлення контакту")]
    [Required]
    [StringLength(1024)]
    public string MakingContact { get; set; }

    [Display(Name = "Результати супервізії")]
    [Required]
    [StringLength(1024)]
    public string SupervisionResults { get; set; }

    [Display(Name = "Складнощі у роботі з супервізантом")]
    [Required]
    [StringLength(1024)]
    public string Difficulties { get; set; }

    [Display(Name = "Домовленість про наступну супервізію")]
    [Required]
    [StringLength(1024)]
    public string NextSupervisionArrangement { get; set; }

    [Display(Name = "Оцінка")]
    public int? SupervisorScore { get; set; }

    [Display(Name = "Супервізія тривала менше 30 хв")]
    public bool SupervisorLessThirtyMinutes { get; set; }
  }
}