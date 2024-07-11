using System;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Shared
{
	public static class PatientViewModelFactory
	{
		public static PatientViewModel Create(Patient patient)
		{
			return new PatientViewModel
			{
				Id = patient.Id,
				FullName = $"{patient.LastName} {patient.FirstName} {patient.MiddleName}",
				Phone = patient.Phone,
				Gender = patient.Gender.GetDisplayName(),
				Birthday = patient.Birthday.ToString("dd.MM.yyyy"),
				Created = patient.Created.ToString("dd.MM.yyyy")
			};
		}
	}
}

