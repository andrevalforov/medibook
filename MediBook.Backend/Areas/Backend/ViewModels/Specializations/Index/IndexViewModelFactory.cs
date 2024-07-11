using System;
using System.Collections.Generic;
using System.Linq;
using MediBook.Backend.ViewModels.Shared;
using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Specializations
{
	public static class IndexViewModelFactory
	{
		public static IndexViewModel Create(IEnumerable<Specialization> specializations, string sorting, int offset, int limit, int total)
		{
			return new IndexViewModel
			{
				Specializations = specializations.Select(SpecializationViewModelFactory.Create),
				Sorting = sorting,
				Offset = offset,
				Limit = limit,
				Total = total
			};
		}
	}
}

