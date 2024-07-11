using System;
using System.Collections.Generic;

namespace MediBook.ViewModels.Shared
{
	public class DoctorViewModel
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public OrganizationViewModel Organization { get; set; }
		public string City { get; set; }
    public IEnumerable<SpecializationViewModel> Specializations { get; set; }
    public string PhotoUrl { get; set; }
    public string About { get; set; }
		public string Room { get; set; }
		public bool IsAvailableIn24Hours { get; set; }
		public bool IsAvailableOnline { get; set; }
  }
}

