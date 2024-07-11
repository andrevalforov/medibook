using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;
using Platformus.Core.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediBook.ViewModels.Account
{
  public class SignUpPageViewModel
  {
    [Display(Name = "Ім'я")]
    [Required(ErrorMessage = "Поле не може бути пустим")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string FirstName { get; set; }

    [Display(Name = "Прізвище")]
    [Required(ErrorMessage = "Поле не може бути пустим")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string LastName { get; set; }

    [Display(Name = "По-батькові")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string MiddleName { get; set; }

    [Display(Name = "Електронна пошта")]
    [Required(ErrorMessage = "Поле не може бути пустим")]
    [EmailAddress(ErrorMessage = "Некоректний формат пошти")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string Email { get; set; }

    [Display(Name = "Стать")]
    [Required]
    public Gender Gender { get; set; }
    public IEnumerable<Option> GenderOptions { get; set; }

    [Display(Name = "Дата народження")]
    [Required]
    public DateTime Birthday { get; set; }

    [Display(Name = "Особливі відмітки (хронічні захворювання тощо)")]
    public string About { get; set; }

    [Display(Name = "Телефон")]
    [Required]
    [Phone(ErrorMessage = "Некоректний формат телефону")]
    public string Phone { get; set; }

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Поле не може бути пустим")]
    [MinLength(8, ErrorMessage = "Мінімальна довжина пароля 8 символів")]
    [StringLength(64, ErrorMessage = "Довжина поля повинна бути не більше 64 символів")]
    public string Password { get; set; }

    [Display(Name = "Пароль ще раз")]
    [Compare("Password", ErrorMessage = "Паролі не співпадають")]
    public string ConfirmPassword { get; set; }
  }
}