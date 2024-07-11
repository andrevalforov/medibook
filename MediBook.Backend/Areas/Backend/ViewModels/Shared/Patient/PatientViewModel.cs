using System;
namespace MediBook.Backend.ViewModels.Shared
{
	public class PatientViewModel
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public string Phone { get; set; }
    public string Gender { get; set; }
		public string Birthday { get; set; }
		public string Created { get; set; }
	}
}

