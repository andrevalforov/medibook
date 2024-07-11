using Magicalizer.Data.Entities.Abstractions;

namespace MediBook.Data.Entities
{
  public class DoctorSpecialization : IEntity
  {
    public int DoctorId { get; set; }
    public int SpecializationId { get; set; }

    public virtual Doctor Doctor { get; set; }
    public virtual Specialization Specialization { get; set; }
  }
}