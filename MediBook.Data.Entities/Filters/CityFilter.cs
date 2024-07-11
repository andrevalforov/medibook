using System;
using Magicalizer.Filters.Abstractions;

namespace MediBook.Data.Entities.Filters
{
	public class CityFilter : IFilter
	{
		public int? RegionId { get; set; }

		public CityFilter()
		{
		}

    public CityFilter(int? regionId = null)
    {
			this.RegionId = regionId;
    }
  }
}

