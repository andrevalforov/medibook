using System.Collections.Generic;
using Magicalizer.Data.Entities.Abstractions;

namespace MediBook.Data.Entities
{
  public class Region : IEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<City> Cities { get; set; }
  }
}
