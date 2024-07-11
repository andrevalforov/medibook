using System;
namespace MediBook.Backend.ViewModels.Shared
{
	public class ConsultationViewModel
	{
		public int Id { get; set; }
		public string Doctor { get; set; }
		public string Patient { get; set; }
		public string Scheduled { get; set; }
		public string Status { get; set; }
	}
}

