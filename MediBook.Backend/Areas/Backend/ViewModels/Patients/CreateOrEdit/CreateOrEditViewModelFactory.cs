using System;
using System.Collections.Generic;
using System.Linq;
using MediBook.Data.Entities;
using Platformus.Core.Primitives;

namespace MediBook.Backend.ViewModels.Patients
{
	public static class CreateOrEditViewModelFactory
	{
		public static CreateOrEditViewModel Create(Patient patient)
		{
			if (patient is null)
				return new CreateOrEditViewModel
				{
					GenderOptions = GetGenderOptions(),
					Birthday = new DateTime(2000, 1, 1),
					IsActivated = true
        };

			return new CreateOrEditViewModel
			{
				Id = patient.Id,
				About = patient.About,
				Birthday = patient.Birthday,
				Created = patient.Created,
				Email = patient.Email,
				FirstName = patient.FirstName,
				Gender = (int)patient.Gender,
				GenderOptions = GetGenderOptions(),
				IsActivated = patient.IsActivated,
				LastName = patient.LastName,
				MiddleName = patient.MiddleName,
				Phone = patient.Phone,
			};
		}

    private static IEnumerable<Option> GetGenderOptions() =>
      (Enum.GetValues(typeof(Gender)) as IEnumerable<Gender>)
      .Select(t => new Option(t.GetDisplayName(), ((int)t).ToString()));

  }
}

