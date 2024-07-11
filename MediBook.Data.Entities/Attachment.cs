using System;
using Magicalizer.Data.Entities.Abstractions;

namespace MediBook.Data.Entities
{
	public class Attachment : IEntity
	{
		public int Id { get; set; }
		public int ConsultationId { get; set; }
		public int? DoctorId { get; set; }
		public int? PatientId { get; set; }
    public string File { get; set; }
		public string Comment { get; set; }
		public DateTime Created { get; set; }

		public virtual Consultation Consultation { get; set; }
		public virtual Doctor Doctor { get; set; }
		public virtual Patient Patient { get; set; }
  }
}

