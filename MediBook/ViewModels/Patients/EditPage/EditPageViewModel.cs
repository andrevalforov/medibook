using MediBook.ViewModels.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediBook.ViewModels.Patients
{
  public class EditPageViewModel
  {

    [Display(Name = "Ім'я")]
    [Required(ErrorMessage = "Поле не може бути порожнім")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string FirstName { get; set; }

    [Display(Name = "Прізвище")]
    [Required(ErrorMessage = "Поле не може бути порожнім")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string LastName { get; set; }

    [Display(Name = "По-батькові")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string MiddleName { get; set; }

    [Display(Name = "Особливі відмітки")]
    [StringLength(2048, ErrorMessage = "Довжина поля повинна бути не більше 2048 символів")]
    public string About { get; set; }

    [Display(Name = "Телефон")]
    [Required(ErrorMessage = "Поле не може бути порожнім")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string Phone { get; set; }

    [Display(Name = "Ваш пароль")]
    public string OldPassword { get; set; }

    [Display(Name = "Новий пароль")]
    [MinLength(8, ErrorMessage = "Мінімальна довжина пароля 8 символів")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string NewPassword { get; set; }

    [Display(Name = "Новий пароль ще раз")]
    [Compare("NewPassword", ErrorMessage = "Паролі не співпадають")]
    public string ConfirmPassword { get; set; }
  }
}