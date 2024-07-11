using System;
using Magicalizer.Filters.Abstractions;

namespace MediBook.Data.Entities.Filters
{
	public class OrganizationFilter : IFilter
	{
		public CityFilter City { get; set; }
		public StringFilter Name { get; set; }

		public OrganizationFilter()
		{
		}

    public OrganizationFilter(CityFilter city = null, StringFilter name = null)
    {
			this.City = city;
			this.Name = name;
    }
  }
}

