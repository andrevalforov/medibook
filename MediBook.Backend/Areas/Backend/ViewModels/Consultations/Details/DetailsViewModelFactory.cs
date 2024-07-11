using System;
using System.Linq;
using MediBook.Backend.ViewModels.Shared;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Consultations
{
	public static class DetailsViewModelFactory
	{
		public static DetailsViewModel Create(Consultation consultation)
		{
			return new DetailsViewModel
			{
				Id = consultation.Id,
				DoctorId = consultation.DoctorId,
				DoctorName = consultation.Doctor.FullName,
				DoctorSpecialization = string.Join(", ", consultation.Doctor.Specializations.Select(sp => sp.Specialization.Name)),
				PatientId = consultation.PatientId,
				PatientName = consultation.Patient is not null
					? $"{consultation.Patient.LastName} {consultation.Patient.FirstName} {consultation.Patient.MiddleName}"
					: null,
				Scheduled = consultation.Scheduled.ToString("dd MMMM yyyy HH:mm"),
				Status = consultation.Status.GetDisplayName(),
				Score = consultation.Score,
				ReasonForAppeal = consultation.ReasonForAppeal,
				DoctorComment = consultation.DoctorComment,
				Diagnosis = consultation.Diagnosis,
				DoctorNotes = consultation.DoctorNotes,
				DoctorRecommendations = consultation.DoctorRecommendations,
				Attachments = consultation.Attachments.Select(AttachmentViewModelFactory.Create)
			};
		}
	}
}

