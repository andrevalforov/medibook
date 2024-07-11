using System;
using System.Collections.Generic;
using MediBook.ViewModels.Shared;
using Platformus.Core.Primitives;

namespace MediBook.ViewModels.Doctors
{
	public class DoctorsPageViewModel
	{
		public IEnumerable<DoctorViewModel> Doctors { get; set; }
		public IEnumerable<Option> SpecializationOptions { get; set; }
		public IEnumerable<Option> RegionOptions { get; set; }
		public IEnumerable<Option> IsOnlineOptions { get; set; }
  }
}

