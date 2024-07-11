﻿using System;
using System.ComponentModel.DataAnnotations;
using Platformus.Core.Backend.ViewModels;

namespace MediBook.Backend.ViewModels.Specializations
{
	public class CreateOrEditViewModel : ViewModelBase
	{
		public int? Id { get; set; }

    [Display(Name = "Назва")]
    [Required]
    [StringLength(64)]
    public string Name { get; set; }

    [Display(Name = "Позиція")]
    public int? Position { get; set; }
  }
}

