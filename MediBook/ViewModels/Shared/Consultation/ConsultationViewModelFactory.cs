using System;
using System.Linq;
using System.Threading.Tasks;
using MediBook.Data.Entities;
using MediBook.Data.Entities.Filters;
using Microsoft.AspNetCore.Http;
using Platformus;

namespace MediBook.ViewModels.Shared
{
  public static class ConsultationViewModelFactory
  {
    public static async Task<ConsultationViewModel> Create(HttpContext httpContext, Consultation consultation, bool includeAttachments = false)
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
        IsOnline = consultation.IsOnline,
        Link = consultation.Link,
        Attachments = includeAttachments
          ? (await httpContext.GetStorage().GetRepository<int, Attachment, AttachmentFilter>()
            .GetAllAsync(new() { ConsultationId = consultation.Id }, "-created"))
            .Select(AttachmentViewModelFactory.Create)
          : null
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