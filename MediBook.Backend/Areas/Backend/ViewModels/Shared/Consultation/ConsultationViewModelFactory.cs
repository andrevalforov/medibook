using System;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Shared
{
	public static class ConsultationViewModelFactory
	{
		public static ConsultationViewModel Create(Consultation consultation)
		{
			return new ConsultationViewModel
			{
				Id = consultation.Id,
				Doctor = consultation.Doctor.FullName,
				Patient = consultation.Patient is not null
					? $"{consultation.Patient.LastName} {consultation.Patient.FirstName} {consultation.Patient.MiddleName}"
					: null,
				Scheduled = consultation.Scheduled.ToString("dd.MM.yyyy"),
				Status = consultation.Status.GetDisplayName()
			};
		}
	}
}

