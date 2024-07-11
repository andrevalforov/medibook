using System;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Shared
{
	public static class AttachmentViewModelFactory
	{
		public static AttachmentViewModel Create(Attachment attachment)
		{
			return new AttachmentViewModel
			{
				Id = attachment.Id,
				Sender = attachment.DoctorId.HasValue
					? attachment.Doctor.FullName
					: $"{attachment.Patient.LastName} {attachment.Patient.FirstName} {attachment.Patient.MiddleName}",
				FileUrl = attachment.File,
				Comment = attachment.Comment,
				Created = attachment.Created.ToString("dd.MM.yyyy HH:mm")
			};
		}
	}
}

