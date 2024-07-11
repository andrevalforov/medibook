using System;
using Magicalizer.Filters.Abstractions;

namespace MediBook.Data.Entities.Filters
{
	public class ConsultationFilter : IFilter
	{
		public int? DoctorId { get; set; }
		public int? PatientId { get; set; }
		public IntegerFilter Status { get; set; }
		public DateTimeFilter Scheduled { get; set; }

		public ConsultationFilter()
		{
		}
	}
}

