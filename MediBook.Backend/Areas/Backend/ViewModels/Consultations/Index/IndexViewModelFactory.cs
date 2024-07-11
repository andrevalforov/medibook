using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Filters.Abstractions;
using MediBook.Backend.ViewModels.Shared;
using MediBook.Data.Entities;
using Microsoft.AspNetCore.Http;
using Platformus;
using Platformus.Core.Primitives;

namespace MediBook.Backend.ViewModels.Consultations
{
	public static class IndexViewModelFactory
	{
		public static async Task<IndexViewModel> CreateAsync(IEnumerable<Consultation> consultations, HttpContext httpContext, string sorting, int offset, int limit, int total)
		{
			return new IndexViewModel
			{
				Consultations = consultations.Select(ConsultationViewModelFactory.Create),
				StatusOptions = GetStatusOptions(),
				DoctorOptions = await GetDoctorOptions(httpContext),
				Sorting = sorting,
				Offset = offset,
				Limit = limit,
				Total = total
			};
		}

    private static IEnumerable<Option> GetStatusOptions() =>
      (Enum.GetValues(typeof(ConsultationStatus)) as IEnumerable<ConsultationStatus>)
      .Select(t => new Option(t.GetDisplayName(), ((int)t).ToString()))
			.Prepend(new Option("Будь-який статус", string.Empty));

		private static async Task<IEnumerable<Option>> GetDoctorOptions(HttpContext httpContext) =>
			(await httpContext.GetStorage().GetRepository<int, Doctor, IFilter>().GetAllAsync(sorting: "+fullname"))
			.Select(d => new Option(d.FullName, d.Id.ToString()))
			.Prepend(new Option("Усі лікарі", string.Empty));
  }
}

