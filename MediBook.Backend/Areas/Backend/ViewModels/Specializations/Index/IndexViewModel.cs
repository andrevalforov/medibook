using System;
using System.Collections.Generic;
using MediBook.Backend.ViewModels.Shared;

namespace MediBook.Backend.ViewModels.Specializations
{
	public class IndexViewModel : IndexViewModelBase
	{
		public IEnumerable<SpecializationViewModel> Specializations { get; set; }
	}
}

