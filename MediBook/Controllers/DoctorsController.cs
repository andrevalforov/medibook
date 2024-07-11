using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hangfire;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using MediBook.Data.Entities;
using MediBook.Data.Entities.Filters;
using MediBook.Services.Abstractions;
using MediBook.ViewModels.Doctors;
using Microsoft.EntityFrameworkCore;
using MediBook.ViewModels.Default;
using System.Numerics;

namespace MediBook.Controllers
{
  public class DoctorsController : WebControllerBase
  {
    //private readonly IEmailService emailService;

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
    private IRepository<int, int, DoctorSpecialization, DoctorSpecializationFilter> DoctorSpecializationRepository
    {
      get => this.Storage.GetRepository<int, int, DoctorSpecialization, DoctorSpecializationFilter>();
    }

    public DoctorsController(IStorage storage, IServiceProvider serviceProvider)
      : base(storage)
    {
      //this.emailService = serviceProvider.GetRequiredService<IEmailService>();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Doctors([FromQuery] int? specializationId = null, [FromQuery] int? regionId = null, [FromQuery] int? isOnline = null)
    {
      return this.View("DoctorsPage", await DoctorsPageViewModelFactory.Create(
        await (Storage.StorageContext as DbContext)
          .Set<Doctor>()
          .Include(d => d.Organization.City)
          .Include("Specializations.Specialization")
          .Include(d => d.Consultations.Where(c => c.Scheduled > DateTime.Now && c.Scheduled < DateTime.Now.AddDays(1) && c.Status == ConsultationStatus.Available))
          .Where(d => d.IsActivated && !d.IsHidden
            && (!specializationId.HasValue || d.Specializations.Any(sp => sp.SpecializationId == specializationId))
            && (!regionId.HasValue || d.Organization.City.RegionId == regionId)
            && (!isOnline.HasValue || isOnline == 1 && d.IsAvailableOnline))
          .ToListAsync(),
        HttpContext));
    }

    [HttpGet]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> CurrentDoctorPage()
    {
      return this.View("CurrentDoctorPage", DoctorPageViewModelFactory.Create(
        HttpContext,
        await (Storage.StorageContext as DbContext)
          .Set<Doctor>()
          .Include(d => d.Organization.City.Region)
          .Include("Specializations.Specialization")
          .Include(d => d.Consultations.Where(c => c.Scheduled > DateTime.Now && c.Scheduled < DateTime.Now.AddDays(1) && c.Status == ConsultationStatus.Available))
          .Where(d => d.Id == GetCurrentUserId())
          .FirstAsync(),
        false));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> DoctorPage(int id)
    {
      return this.View("DoctorPage", DoctorPageViewModelFactory.Create(
        HttpContext,
        await (Storage.StorageContext as DbContext)
          .Set<Doctor>()
          .Include(d => d.Organization.City.Region)
          .Include("Specializations.Specialization")
          .Include(d => d.Consultations.Where(c => c.Scheduled > DateTime.Now && c.Scheduled < DateTime.Now.AddDays(1) && c.Status == ConsultationStatus.Available))
          .Where(d => d.Id == id && d.IsActivated)
          .FirstAsync(),
        true));
    }

    [HttpGet]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> CurrentDoctorConsultations()
    {
      return this.View("CurrentDoctorConsultationsPage", DoctorConsultationsPageViewModelFactory.Create(
        HttpContext,
        await (Storage.StorageContext as DbContext)
          .Set<Doctor>()
          .Include(d => d.Consultations.Where(c => c.Status != ConsultationStatus.Available))
          .ThenInclude(c => c.Patient)
          .Where(d => d.Id == GetCurrentUserId())
          .FirstAsync()));
    }

    [Authorize(Roles = "Doctor")]
    [HttpGet]
    public async Task<IActionResult> Edit()
    {
      Doctor doctor = await DoctorRepository.GetByIdAsync(GetCurrentUserId());

      return this.View("EditPage", EditPageViewModelFactory.Create(HttpContext, doctor));
    }

    [Authorize(Roles = "Doctor")]
    [HttpPost]
    public async Task<IActionResult> Edit(EditPageViewModel editPageViewModel)
    {
      if (this.ModelState.IsValid)
      {
        Doctor doctor = await DoctorRepository.GetByIdAsync(GetCurrentUserId());

        doctor = EditPageViewModelMapper.Map(editPageViewModel, doctor);

        DoctorRepository.Edit(doctor);
        await this.Storage.SaveAsync();

        return this.Redirect("/doctors/me");
      }

      return this.View("EditPage", EditPageViewModelFactory.Create(editPageViewModel));
    }

    [Authorize(Roles = "Doctor")]
    [HttpGet]
    [ActionName("Schedule")]
    public async Task<IActionResult> ScheduleGet(int year, int month, int day)
    {
      Doctor doctor = await DoctorRepository.GetByIdAsync(GetCurrentUserId());

      return this.View("SchedulePage", await SchedulePageViewModelFactory.Create(HttpContext, doctor, new DateTime(year, month, day)));
    }

    [Authorize(Roles = "Doctor")]
    [HttpPost]
    [ActionName("Schedule")]
    public async Task<IActionResult> SchedulePost(int year, int month, int day)
    {
      Doctor doctor = await DoctorRepository.GetByIdAsync(GetCurrentUserId());

      if (DateTime.Now.Date > new DateTime(year, month, day))
        return this.BadRequest();

      await this.CreateOrEditConsultations(doctor, year, month, day);
      return this.Redirect("/doctors/me/consultations");
    }

    [Authorize(Roles = "Doctor")]
    [HttpPost]
    public async Task<IActionResult> CancelConsultation(int id)
    {
      Consultation consultation = await ConsultationRepository.GetByIdAsync(id);

      if (consultation.DoctorId != GetCurrentUserId())
        return this.Unauthorized();

      if (consultation.Scheduled < DateTime.Now.AddHours(-1))
        return this.Unauthorized();

      consultation.Status = ConsultationStatus.Canceled;

      ConsultationRepository.Edit(consultation);
      await this.Storage.SaveAsync();

      //this.SendEmails(supervision, "SupervisionCanceledBySupervisor");
      return this.Ok();
    }

    [Authorize(Roles = "Patient")]
    [HttpGet]
    public async Task<IActionResult> SchedulePatient(int id, int year, int month, int day)
    {
      Doctor doctor = await DoctorRepository.GetByIdAsync(id);

      return this.View("DoctorScheduleForPatientPage", await ViewModels.Shared.SchedulePageViewModelFactory.Create(HttpContext, doctor, new DateTime(year, month, day), GetCurrentUserId()));
    }

    [Authorize(Roles = "Patient")]
    [HttpPost]
    public async Task<IActionResult> Book(int id, [FromQuery] string reason, [FromQuery] bool? isOnline)
    {
      Consultation consultation = await ConsultationRepository.GetByIdAsync(id);

      if (consultation.Status != ConsultationStatus.Available || consultation.Scheduled < DateTime.Now)
        return NotFound();

      consultation.Status = ConsultationStatus.Booked;
      consultation.ReasonForAppeal = reason;
      consultation.PatientId = GetCurrentUserId();
      consultation.IsOnline = isOnline ?? false;

      ConsultationRepository.Edit(consultation);
      await Storage.SaveAsync();

      return Redirect("/patients/me/consultations");
    }
    
    private async Task CreateOrEditConsultations(Doctor doctor, int year, int month, int day)
    {
      IEnumerable<Consultation> consultations = await ConsultationRepository.GetAllAsync(new() { DoctorId = doctor.Id, Scheduled = new DateTimeFilter(equals: new DateTime(year, month, day)) });

      foreach (var consultation in consultations)
        if (!this.DoesConsultationExistInForm(consultation))
          ConsultationRepository.Delete(consultation.Id);

      await this.Storage.SaveAsync();

      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("supervision"))
        {
          DateTime scheduled = DateTime.Parse(key.Replace("supervision", string.Empty));

          if (!consultations.Any(s => s.Scheduled == scheduled))
            this.CreateConsultation(doctor, scheduled);
        }
      }

      this.Storage.Save();
    }

    private bool DoesConsultationExistInForm(Consultation consultation)
    {
      foreach (string key in this.Request.Form.Keys)
        if (key.StartsWith("supervision"))
          if (consultation.Scheduled == DateTime.Parse(key.Replace("supervision", string.Empty)))
            return true;

      return false;
    }

    private void CreateConsultation(Doctor doctor, DateTime scheduled)
    {
      Consultation consultation = new()
      {
        DoctorId = doctor.Id,
        Scheduled = scheduled,
        Status = ConsultationStatus.Available
      };

      ConsultationRepository.Create(consultation);
    }
  }
}