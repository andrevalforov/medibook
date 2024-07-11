using System;
using System.Collections.Generic;
using System.Linq;
using MediBook.Backend.ViewModels.Shared;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Doctors
{
	public static class IndexViewModelFactory
	{
		public static IndexViewModel Create(IEnumerable<Doctor> doctors, string sorting, int offset, int limit, int total)
		{
			return new IndexViewModel
			{
				Doctors = doctors.Select(DoctorViewModelFactory.Create),
				Sorting = sorting,
				Offset = offset,
				Limit = limit,
				Total = total
			};
		}
	}
}

