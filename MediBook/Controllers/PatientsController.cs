using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediBook.Data.Entities;
using MediBook.Data.Entities.Filters;
using MediBook.Services.Abstractions;
using MediBook.ViewModels.Patients;
using MediBook.ViewModels.Default;
using Microsoft.AspNetCore.Http;
using MediBook.ViewModels.Shared;

namespace MediBook.Controllers
{
  public class PatientsController : WebControllerBase
  {
    private readonly IEmailService emailService;

    private IRepository<int, Patient, PatientFilter> PatientRepository
    {
      get => this.Storage.GetRepository<int, Patient, PatientFilter>();
    }
    private IRepository<int, Consultation, ConsultationFilter> ConsultationRepository
    {
      get => this.Storage.GetRepository<int, Consultation, ConsultationFilter>();
    }

    public PatientsController(IStorage storage, IServiceProvider serviceProvider)
      : base(storage)
    {
      //this.emailService = serviceProvider.GetRequiredService<IEmailService>();
    }

    [HttpGet]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> MyPage()
    {
      Patient patient = await PatientRepository.GetByIdAsync(GetCurrentUserId());

      return this.View("CurrentPatientPage", PatientPageViewModelFactory.Create(HttpContext, patient, GetCurrentUserId(), withCalendar: true));
    }

    [HttpGet]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> PatientPage(int id)
    {
      Patient patient = await PatientRepository.GetByIdAsync(id, inclusions: new Inclusion<Patient>("Consultations.Doctor"));

      if (!patient.Consultations.Any(c => c.DoctorId == GetCurrentUserId()))
        return Forbid();

      return this.View("PatientPage", PatientPageViewModelFactory.Create(HttpContext, patient, GetCurrentUserId()));
    }

    [HttpGet]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> Edit()
    {
      Patient patient = await PatientRepository.GetByIdAsync(GetCurrentUserId());

      return this.View("EditPage", await EditPageViewModelFactory.CreateAsync(HttpContext, patient));
    }

    [HttpGet]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> MyConsultations()
    {
      Patient patient = await PatientRepository.GetByIdAsync(GetCurrentUserId());

      IEnumerable<Consultation> consultations = await ConsultationRepository.GetAllAsync(new() { PatientId = patient.Id }, inclusions: new Inclusion<Consultation>(c => c.Doctor));
      int currentConsultationsCount = consultations.Count(c => c.Status == ConsultationStatus.Booked && c.Scheduled.AddMinutes(30) > DateTime.Now);
      int archivedConsultationsCount = consultations.Count(c => c.Status != ConsultationStatus.Booked || c.Scheduled.AddMinutes(30) < DateTime.Now);

      return this.View("PatientSchedulePage", new List<ConsultationsScheduleGroupViewModel>
      {
        GetGroup(HttpContext, consultations.Where(c => c.Status == ConsultationStatus.Booked && c.Scheduled.AddMinutes(30) > DateTime.Now), "Current", false),
        GetGroup(HttpContext, consultations.Where(c => c.Status != ConsultationStatus.Booked || c.Scheduled.AddMinutes(30) < DateTime.Now), "Archived", false)
      });
    }

    private static ConsultationsScheduleGroupViewModel GetGroup(HttpContext httpContext, IEnumerable<Consultation> consultations, string code, bool pagination)
    {
      return new ConsultationsScheduleGroupViewModel
      {
        Code = code,
        ShowPagination = pagination,
        Consultations = consultations
          .GroupBy(s => s.Scheduled.Date)
          .Select(g => new ConsultationsScheduleViewModel
          {
            Date = g.Key,
            Consultations = g.Select(s => ViewModels.Shared.ConsultationViewModelFactory.Create(httpContext, s).Result)
          })
      };
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditPageViewModel editPageViewModel)
    {
      if (this.ModelState.IsValid)
      {
        Patient patient = await PatientRepository.GetByIdAsync(GetCurrentUserId());
        patient = EditPageViewModelMapper.Map(editPageViewModel, patient);

        PatientRepository.Edit(patient);
        await this.Storage.SaveAsync();

        return this.Redirect($"/patients/me");
      }

      return this.View("EditPage", EditPageViewModelFactory.Create(editPageViewModel));
    }

    [HttpGet]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> Schedule(int year, int month, int day)
    {
      Patient patient = await PatientRepository.GetByIdAsync(GetCurrentUserId());

      return this.View("SchedulePage", await ViewModels.Supervisees.SchedulePageViewModelFactory.Create(HttpContext, patient, new DateTime(year, month, day)));
    }

    [Authorize(Roles = "Patient")]
    [HttpPost]
    public async Task<IActionResult> CancelConsultation(int id)
    {
      Consultation consultation = await ConsultationRepository.GetByIdAsync(id);

      if (consultation.PatientId != GetCurrentUserId())
        return this.Unauthorized();

      if (consultation.Scheduled < DateTime.Now.AddHours(-1))
        return this.Unauthorized();

      consultation.Status = ConsultationStatus.Available;
      consultation.PatientId = null;

      ConsultationRepository.Edit(consultation);
      await this.Storage.SaveAsync();

      return this.Ok();
    }
  }
}