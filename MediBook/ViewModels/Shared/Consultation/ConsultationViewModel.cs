using System;
using System.Collections.Generic;
using MediBook.Data.Entities;

namespace MediBook.ViewModels.Shared
{
  public class ConsultationViewModel
  {
    public int Id { get; set; }
    public DoctorViewModel Doctor { get; set; }
    public PatientViewModel Patient { get; set; }
    public DateTime Scheduled { get; set; }
    public ConsultationStatus? Status { get; set; }
    public string ReasonForAppeal { get; set; }
    public string DoctorNotes { get; set; }
    public string DoctorComment { get; set; }
    public string DoctorRecommendations { get; set; }
    public string Diagnosis { get; set; }
    public bool IsOnline { get; set; }
    public string Link { get; set; }
    public IEnumerable<AttachmentViewModel> Attachments { get; set; }
  }
}