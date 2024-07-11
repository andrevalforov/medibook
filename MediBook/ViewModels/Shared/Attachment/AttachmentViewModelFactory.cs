using System;
using MediBook.Data.Entities;

namespace MediBook.ViewModels.Shared
{
	public static class AttachmentViewModelFactory
	{
		public static AttachmentViewModel Create(Attachment attachment)
		{
			return new AttachmentViewModel
			{
				DoctorId = attachment.DoctorId,
				PatientId = attachment.PatientId,
				Author = attachment.DoctorId.HasValue ? attachment.Doctor.FullName : $"{attachment.Patient.LastName} {attachment.Patient.FirstName} {attachment.Patient.MiddleName}",
				Text = attachment.Comment,
				File = attachment.File,
				Created = attachment.Created
			};
		}
	}
}

