﻿using System;
using MediBook.Data.Entities;

namespace MediBook.ViewModels.Shared
{
	public static class SpecializationViewModelFactory
	{
		public static SpecializationViewModel Create(Specialization specialization)
		{
			return new SpecializationViewModel
			{
				Id = specialization.Id,
				Name = specialization.Name
			};
		}
	}
}

