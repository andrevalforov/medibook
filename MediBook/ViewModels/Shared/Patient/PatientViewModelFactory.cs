using System;
using MediBook.Data.Entities;

namespace MediBook.ViewModels.Shared
{
	public static class PatientViewModelFactory
	{
		public static PatientViewModel Create(Patient patient)
		{
			return new PatientViewModel
			{
				Id = patient.Id,
				FullName = $"{patient.LastName} {patient.FirstName} {patient.MiddleName}",
				Birthday = patient.Birthday,
				Gender = patient.Gender.GetDisplayName(),
				Phone = patient.Phone
			};
		}
	}
}

