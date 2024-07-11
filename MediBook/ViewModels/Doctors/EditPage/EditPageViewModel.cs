using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediBook.ViewModels.Shared;

namespace MediBook.ViewModels.Doctors
{
  public class EditPageViewModel
  {
    [Display(Name = "Повне ім'я")]
    [Required(ErrorMessage = "Поле не може бути пустим")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string FullName { get; set; }

    [Display(Name = "Про мене")]
    [StringLength(2048, ErrorMessage = "Довжина поля повинна бути не більше 2048 символів")]
    public string About { get; set; }

    [Display(Name = "Професійний досвід")]
    [StringLength(2048, ErrorMessage = "Довжина поля повинна бути не більше 2048 символів")]
    public string ProfessionalExperience { get; set; }

    [Display(Name = "Професійне навчання")]
    [StringLength(2048, ErrorMessage = "Довжина поля повинна бути не більше 2048 символів")]
    public string ProfessionalEducation { get; set; }

    [Display(Name = "Кабінет")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string Room { get; set; }

    [Display(Name = "Можливий прийом онлайн")]
    public bool IsAvailableOnline { get; set; }

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