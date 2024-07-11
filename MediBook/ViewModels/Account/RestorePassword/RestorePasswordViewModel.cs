using System.ComponentModel.DataAnnotations;

namespace MediBook.ViewModels.Account
{
  public class RestorePasswordViewModel
  {
    [Display(Name = "Електронна пошта")]
    [Required(ErrorMessage = "Поле не може бути пустим")]
    public string Email { get; set; }
  }
}
