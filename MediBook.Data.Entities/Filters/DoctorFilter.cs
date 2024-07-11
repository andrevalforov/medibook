using System;
using Magicalizer.Filters.Abstractions;

namespace MediBook.Data.Entities.Filters
{
	public class DoctorFilter : IFilter
	{
		public StringFilter FullName { get; set; }
    public StringFilter Email { get; set; }
    public StringFilter Phone { get; set; }
    [FilterShortcut("Specializations[]")]
    public DoctorSpecializationFilter Specialization { get; set; }
    public bool? IsActivated { get; set; }
    public bool? IsHidden { get; set; }
    public bool? IsAvailableOnline { get; set; }
    public OrganizationFilter Organization { get; set; }

    public DoctorFilter()
    {

    }

    public DoctorFilter(StringFilter email = null, StringFilter phone = null, bool ? isActivated = null, bool? isHidden = null, bool? isAvailableOnline = null, DoctorSpecializationFilter specialization = null, OrganizationFilter organization = null)
    {
      this.Email = email;
      this.Phone = phone;
      this.IsActivated = isActivated;
      this.IsHidden = isHidden;
      this.IsAvailableOnline = isAvailableOnline;
      this.Specialization = specialization;
      this.Organization = organization;
    }
  }
}

