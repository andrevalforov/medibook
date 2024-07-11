using System;
using System.Collections.Generic;
using MediBook.Backend.ViewModels.Shared;

namespace MediBook.Backend.ViewModels.Consultations
{
	public class DetailsViewModel
	{
		public int Id { get; set; }
		public int DoctorId { get; set; }
		public string DoctorName { get; set; }
		public string DoctorSpecialization { get; set; }
    public int? PatientId { get; set; }
		public string PatientName { get; set; }
		public string Scheduled { get; set; }
		public string Status { get; set; }
    public string ReasonForAppeal { get; set; }
    public int? Score { get; set; }
    public string DoctorNotes { get; set; }
    public string DoctorComment { get; set; }
    public string DoctorRecommendations { get; set; }
    public string Diagnosis { get; set; }

		public IEnumerable<AttachmentViewModel> Attachments { get; set; }
	}
}

