using Magicalizer.Data.Entities.Abstractions;
using System;

namespace MediBook.Data.Entities
{
  public class RestorePasswordToken : IEntity
  {
    public Guid Id { get; set; }
    public int? PatientId { get; set; }
    public int? DoctorId { get; set; }
    public DateTime? Used { get; set; }
    public DateTime Created { get; set; }

    public virtual Patient Patient { get; set; }
    public virtual Doctor Doctor { get; set; }
  }
}
