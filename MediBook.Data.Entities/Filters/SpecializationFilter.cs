using System;
using Magicalizer.Filters.Abstractions;

namespace MediBook.Data.Entities.Filters
{
	public class SpecializationFilter : IFilter
	{
		public int? Id { get; set; }
		public string Name { get; set; }

    public SpecializationFilter()
		{
		}

    public SpecializationFilter(int? id = null, string name = null)
    {
			this.Id = id;
			this.Name = name;
    }
  }
}

