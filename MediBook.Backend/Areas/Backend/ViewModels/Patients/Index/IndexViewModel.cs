using System;
using System.Collections.Generic;
using MediBook.Backend.ViewModels.Shared;

namespace MediBook.Backend.ViewModels.Patients
{
	public class IndexViewModel : IndexViewModelBase
	{
		public IEnumerable<PatientViewModel> Patients { get; set; }
	}
}

