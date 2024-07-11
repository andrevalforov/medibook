using System;
using System.Collections.Generic;
using Magicalizer.Data.Entities.Abstractions;

namespace MediBook.Data.Entities
{
  public class Doctor : IEntity
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Secret { get; set; }
    public string Extra { get; set; }
    public string About { get; set; }
    public string ProfessionalExperience { get; set; }
    public string ProfessionalEducation { get; set; }
    public string PhotoUrl { get; set; }
    public int OrganizationId { get; set; }
    public bool IsActivated { get; set; }
    public bool IsHidden { get; set; }
    public bool IsAvailableOnline { get; set; }
    public string Room { get; set; }
    public DateTime Created { get; set; }

    public virtual Organization Organization { get; set; }
    public virtual ICollection<Consultation> Consultations { get; set; }
    public virtual ICollection<DoctorSpecialization> Specializations { get; set; }
  }
}