using System.ComponentModel.DataAnnotations;

namespace MediBook.ViewModels.Account
{
  public class SignInPageViewModel
  {
    [Display(Name = "Електронна пошта")]
    [Required (ErrorMessage = "Поле не може бути пустим")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string Email { get; set; }

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Поле не може бути пустим")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
  }
}