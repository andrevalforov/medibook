using System.Collections.Generic;
using Magicalizer.Data.Entities.Abstractions;
using Platformus.Core.Data.Entities;

namespace MediBook.Data.Entities
{
  public class Specialization : IEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }
    
    public virtual ICollection<DoctorSpecialization> Topics { get; set; }
  }
}