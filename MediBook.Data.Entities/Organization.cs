using Magicalizer.Data.Entities.Abstractions;
using System.Collections.Generic;

namespace MediBook.Data.Entities
{
  public class Organization : IEntity
  {
    public int Id { get; set; }
    public int CityId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public virtual City City { get; set; }
    public IEnumerable<Doctor> Doctors { get; set; }
  }
}
