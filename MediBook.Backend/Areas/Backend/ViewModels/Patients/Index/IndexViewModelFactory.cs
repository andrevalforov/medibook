using System;
using System.Collections.Generic;
using System.Linq;
using MediBook.Backend.ViewModels.Shared;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Patients
{
	public static class IndexViewModelFactory
	{
		public static IndexViewModel Create(IEnumerable<Patient> patients, string sorting, int offset, int limit, int total)
		{
			return new IndexViewModel
			{
				Patients = patients.Select(PatientViewModelFactory.Create),
				Sorting = sorting,
				Offset = offset,
				Limit = limit,
				Total = total
			};
		}
	}
}

