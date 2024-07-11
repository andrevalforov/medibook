using System;
using System.Collections.Generic;
using MediBook.Backend.ViewModels.Shared;

namespace MediBook.Backend.ViewModels.Doctors
{
	public class IndexViewModel : IndexViewModelBase
	{
		public IEnumerable<DoctorViewModel> Doctors { get; set; }
	}
}

