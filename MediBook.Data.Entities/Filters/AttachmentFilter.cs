using System;
using Magicalizer.Filters.Abstractions;

namespace MediBook.Data.Entities.Filters
{
	public class AttachmentFilter : IFilter
	{
		public int? ConsultationId { get; set; }

		public AttachmentFilter()
		{
		}
	}
}

