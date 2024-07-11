using System;
using System.IO;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using MediBook.Data.Entities;
using MediBook.Data.Entities.Filters;
using MediBook.ViewModels.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace MediBook.Controllers
{
  public class ConsultationsController : WebControllerBase
  {
    private IRepository<int, Patient, PatientFilter> PatientRepository
    {
      get => this.Storage.GetRepository<int, Patient, PatientFilter>();
    }
    private IRepository<int, Consultation, ConsultationFilter> ConsultationRepository
    {
      get => this.Storage.GetRepository<int, Consultation, ConsultationFilter>();
    }
    private IRepository<int, Doctor, DoctorFilter> DoctorRepository
    {
      get => this.Storage.GetRepository<int, Doctor, DoctorFilter>();
    }
    private IRepository<int, Attachment, AttachmentFilter> AttachmentRepository
    {
      get => this.Storage.GetRepository<int, Attachment, AttachmentFilter>();
    }

    private readonly IWebHostEnvironment environment;

    public ConsultationsController(IStorage storage, IWebHostEnvironment environment)
      : base(storage)
    {
      this.environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> Consultation(int id)
    {
      Consultation consultation = await ConsultationRepository.GetByIdAsync(id, new Inclusion<Consultation>[]
      {
        new Inclusion<Consultation>(c => c.Doctor.Organization),
        new Inclusion<Consultation>("Doctor.Specializations.Specialization"),
        new Inclusion<Consultation>(c => c.Patient),
        new Inclusion<Consultation>("Attachments.Patient"),
        new Inclusion<Consultation>("Attachments.Doctor")
      });

      if (GetCurrentUserRole() is "Doctor" && GetCurrentUserId() != consultation.DoctorId
        || GetCurrentUserRole() is "Patient" && GetCurrentUserId() != consultation.PatientId)
        return Forbid();

      return this.View("Consultation", await ConsultationViewModelFactory.Create(consultation, GetCurrentUserRole() is "Doctor"));
    }

    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Consultation(ConsultationViewModel viewModel)
    {
      Consultation consultation = await ConsultationRepository.GetByIdAsync(viewModel.Id);

      if (GetCurrentUserId() != consultation.DoctorId)
        return Forbid();

      consultation.Diagnosis = viewModel.Diagnosis;
      consultation.DoctorNotes = viewModel.DoctorNotes;
      consultation.DoctorRecommendations = viewModel.DoctorRecommendations;
      consultation.DoctorComment = viewModel.DoctorComment;
      consultation.Status = ConsultationStatus.Completed;

      ConsultationRepository.Edit(consultation);
      await Storage.SaveAsync();

      return Redirect("/doctors/me/consultations");
    }

    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> MarkAsCanceled(int id)
    {
      Consultation consultation = await ConsultationRepository.GetByIdAsync(id);

      if (GetCurrentUserId() != consultation.DoctorId)
        return Forbid();

      consultation.Status = ConsultationStatus.Canceled;

      ConsultationRepository.Edit(consultation);
      await Storage.SaveAsync();

      return this.Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddAttachment(int id, IFormFile file, string attachmentComment)
    {
      Consultation consultation = await ConsultationRepository.GetByIdAsync(id);

      if (consultation is null
        || GetCurrentUserRole() is "Doctor" && GetCurrentUserId() != consultation.DoctorId
        || GetCurrentUserRole() is "Patient" && GetCurrentUserId() != consultation.PatientId)
        return Forbid();

      if (file is not null)
      {
        string path = Path.Combine(environment.ContentRootPath, "wwwroot", "attachments", id.ToString());

        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);

        using (FileStream stream = new FileStream(path + $"/{file.FileName}", FileMode.Create))
          await file.CopyToAsync(stream);
      }

      Attachment attachment = new()
      {
        DoctorId = GetCurrentUserRole() is "Doctor" ? GetCurrentUserId() : null,
        PatientId = GetCurrentUserRole() is "Patient" ? GetCurrentUserId() : null,
        ConsultationId = consultation.Id,
        Comment = attachmentComment,
        File = file is not null ? $"/attachments/{id}/{file.FileName}" : null,
        Created = DateTime.Now
      };

      AttachmentRepository.Create(attachment);
      await Storage.SaveAsync();

      return Redirect($"/consultations/{id}");
    }

    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> AddLink(int id, string link)
    {
      Consultation consultation = await ConsultationRepository.GetByIdAsync(id);
      if (consultation is null || GetCurrentUserId() != consultation.DoctorId || !consultation.IsOnline)
        return Forbid();

      consultation.Link = link;
      ConsultationRepository.Edit(consultation);
      await Storage.SaveAsync();

      return Redirect($"/consultations/{id}");
    }
  }
}

