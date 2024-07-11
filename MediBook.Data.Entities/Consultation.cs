using System;
using System.Collections.Generic;
using Magicalizer.Data.Entities.Abstractions;

namespace MediBook.Data.Entities
{
  public enum ConsultationStatus
  {
    Available = 1,
    Booked,
    Canceled,
    Completed
  }

  public class Consultation : IEntity
  {
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int? PatientId { get; set; }
    public DateTime Scheduled { get; set; }
    public ConsultationStatus Status { get; set; }
    public string Link { get; set; }
    public bool IsOnline { get; set; }
    public string ReasonForAppeal { get; set; }
    public int? Score { get; set; }
    public string DoctorNotes { get; set; }
    public string DoctorComment { get; set; }
    public string DoctorRecommendations { get; set; }
    public string Diagnosis { get; set; }
    public DateTime Created { get; set; }

    public virtual Doctor Doctor { get; set; }
    public virtual Patient Patient { get; set; }
    public virtual ICollection<Attachment> Attachments { get; set; }
  }

  public static class ConsultationStatusExtension
  {
    public static string GetDisplayName(this ConsultationStatus status)
    {
      return status switch
      {
        ConsultationStatus.Available => "Вільно",
        ConsultationStatus.Booked => "Заплановано",
        ConsultationStatus.Canceled => "Скасовано",
        ConsultationStatus.Completed => "Завершено",
        _ => string.Empty,
      };
    }
  }
}