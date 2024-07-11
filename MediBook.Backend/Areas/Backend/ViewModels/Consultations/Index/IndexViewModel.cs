using System;
using System.Collections.Generic;
using MediBook.Backend.ViewModels.Shared;
using Platformus.Core.Primitives;

namespace MediBook.Backend.ViewModels.Consultations
{
	public class IndexViewModel : IndexViewModelBase
	{
		public IEnumerable<ConsultationViewModel> Consultations { get; set; }
		public IEnumerable<Option> DoctorOptions { get; set; }
		public IEnumerable<Option> StatusOptions { get; set; }
  }
}

