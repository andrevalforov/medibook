using System;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Specializations
{
	public static class CreateOrEditViewModelFactory
	{
		public static CreateOrEditViewModel Create(Specialization specialization)
		{
			if (specialization is null)
				return new CreateOrEditViewModel();

			return new CreateOrEditViewModel
			{
				Id = specialization.Id,
				Name = specialization.Name,
				Position = specialization.Position
			};
		}
	}
}

