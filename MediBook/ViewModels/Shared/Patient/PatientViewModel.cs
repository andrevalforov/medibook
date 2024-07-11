using System;
namespace MediBook.ViewModels.Shared
{
	public class PatientViewModel
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public string Gender { get; set; }
		public DateTime Birthday { get; set; }
		public string Phone { get; set; }
	}
}

