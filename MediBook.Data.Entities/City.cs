using System.Collections.Generic;
using Magicalizer.Data.Entities.Abstractions;

namespace MediBook.Data.Entities
{
  public class City : IEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? RegionId { get; set; }

    public virtual Region Region { get; set; }
    public virtual ICollection<Organization> Organizations { get; set; }
  }
}
