using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;

namespace MediBook.Backend.ViewModels.Patients
{
	public class CreateOrEditViewModel : ViewModelBase
	{
		public int? Id { get; set; }

    [Display(Name = "Імʼя")]
    [Required]
    public string FirstName { get; set; }

    [Display(Name = "По-батькові")]
    [Required]
    public string MiddleName { get; set; }

    [Display(Name = "Прізвище")]
    [Required]
    public string LastName { get; set; }

    [Display(Name = "Стать")]
    [Required]
    public int Gender { get; set; }
    public IEnumerable<Option> GenderOptions { get; set; }

    [Display(Name = "Дата народження")]
    [Required]
    public DateTime Birthday { get; set; }

    [Display(Name = "Особливі відмітки (хронічні захворювання тощо)")]
    public string About { get; set; }

    [Display(Name = "Email")]
    [Required]
    public string Email { get; set; }

    [Display(Name = "Телефон")]
    [Required]
    public string Phone { get; set; }

    [Display(Name = "Пароль")]
    [MaxLength(64)]
    public string Password { get; set; }

    [Display(Name = "Пароль повторно")]
    [Compare("Password")]
    public string RepeatPassword { get; set; }

    [Display(Name = "Активований")]
    [Required]
    public bool IsActivated { get; set; }

    [Display(Name = "Створено")]
    public DateTime Created { get; set; }
  }
}

