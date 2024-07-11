using System;
using Magicalizer.Filters.Abstractions;

namespace MediBook.Data.Entities.Filters
{
	public class DoctorSpecializationFilter : IFilter
	{
		public int? DoctorId { get; set; }
		public int? SpecializationId { get; set; }

		public DoctorSpecializationFilter()
		{

		}

    public DoctorSpecializationFilter(int? doctorId = null, int? specializationId = null)
		{
			this.DoctorId = doctorId;
			this.SpecializationId = specializationId;
		}
	}
}

