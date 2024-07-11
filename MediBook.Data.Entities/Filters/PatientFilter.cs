using System;
using Magicalizer.Filters.Abstractions;

namespace MediBook.Data.Entities.Filters
{
	public class PatientFilter : IFilter
	{
		public StringFilter Email { get; set; }
		public StringFilter Phone { get; set; }
		public DateTimeFilter Birthday { get; set; }

		public PatientFilter()
		{

		}

		public PatientFilter(StringFilter email = null, StringFilter phone = null)
		{
			this.Email = email;
			this.Phone = phone;
		}
	}
}

