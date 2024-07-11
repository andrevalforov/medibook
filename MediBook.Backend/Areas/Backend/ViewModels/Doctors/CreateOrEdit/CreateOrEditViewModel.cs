using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediBook.Backend.ViewModels.Shared;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;

namespace MediBook.Backend.ViewModels.Doctors
{
	public class CreateOrEditViewModel : ViewModelBase
	{
		public int? Id { get; set; }

    [Display(Name = "Повне ім’я")]
    [Required]
    [StringLength(64)]
    public string FullName { get; set; }

    [Display(Name = "Заклад")]
    [Required]
    public int OrganizationId { get; set; }
    public IEnumerable<Option> OrganizationOptions { get; set; }

    [Display(Name = "Освіта")]
    [StringLength(2048)]
    public string About { get; set; }

    [Display(Name = "Професійний досвід")]
    [StringLength(2048)]
    public string ProfessionalExperience { get; set; }

    [Display(Name = "Професійне навчання")]
    [StringLength(2048)]
    public string ProfessionalEducation { get; set; }

    [Display(Name = "Зображення")]
    [StringLength(256)]
    public string PhotoUrl { get; set; }

    [Display(Name = "Лікар активований")]
    public bool IsActivated { get; set; }

    [Display(Name = "Приховати лікаря")]
    public bool IsHidden { get; set; }

    [Display(Name = "Приймає онлайн")]
    public bool IsAvailableOnline { get; set; }

    [Display(Name = "Email")]
    [Required]
    [MaxLength(64)]
    public string Identifier { get; set; }

    [Display(Name = "Телефон")]
    [Required]
    [MaxLength(64)]
    public string Phone { get; set; }

    [Display(Name = "Пароль")]
    [MaxLength(64)]
    public string Password { get; set; }

    [Display(Name = "Пароль повторно")]
    [Compare("Password")]
    public string RepeatPassword { get; set; }

    [Display(Name = "Кабінет")]
    public string Room { get; set; }

    public DoctorSpecializationViewModel[] DoctorSpecializations { get; set; }
  }
}

