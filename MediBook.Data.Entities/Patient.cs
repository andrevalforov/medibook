using System;
using System.Collections.Generic;
using Magicalizer.Data.Entities.Abstractions;

namespace MediBook.Data.Entities
{
  public class Patient : IEntity
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime Birthday { get; set; }
    public string About { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Secret { get; set; }
    public string Extra { get; set; }
    public bool IsActivated { get; set; }
    public DateTime Created { get; set; }

    public virtual ICollection<Consultation> Consultations { get; set; }
  }

  public enum Gender
  {
    Male,
    Female,
    Other
  }

  public static class GenderExtension
  {
    public static string GetDisplayName(this Gender gender)
    {
      return gender switch
      {
        Gender.Male => "Чоловік",
        Gender.Female => "Жінка",
        Gender.Other => "Інша",
        _ => string.Empty,
      };
    }

  }
}