using System;
namespace MediBook.Backend.ViewModels.Shared
{
	public class DoctorViewModel
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public string Organization { get; set; }
		public string Specializations { get; set; }
		public string Created { get; set; }
	}
}

