using System;
using System.Linq;
using System.Threading.Tasks;
using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;

namespace MediBook.ViewModels.Default
{
  public static class ConsultationViewModelFactory
  {
    public static async Task<ConsultationViewModel> Create(Consultation consultation, bool isDoctor)
    {
      return new ConsultationViewModel()
      {
        Id = consultation.Id,
        Doctor = consultation.Doctor is not null ? DoctorViewModelFactory.Create(consultation.Doctor) : null,
        Patient = consultation.Patient is not null ? PatientViewModelFactory.Create(consultation.Patient) : null,
        Status = consultation.Status,
        Scheduled = consultation.Scheduled,
        ReasonForAppeal = consultation.ReasonForAppeal,
        Diagnosis = consultation.Diagnosis,
        DoctorComment = consultation.DoctorComment,
        DoctorNotes = consultation.DoctorNotes,
        DoctorRecommendations = consultation.DoctorRecommendations,
        IsDoctor = isDoctor,
        IsOnline = consultation.IsOnline,
        Link = consultation.Link,
        Attachments = consultation.Attachments.OrderByDescending(a => a.Created).Select(AttachmentViewModelFactory.Create)
      };
    }

    public static ConsultationViewModel Create(DateTime scheduled)
    {
      return new ConsultationViewModel()
      {
        Scheduled = scheduled
      };
    }
  }
}