using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus;
using Platformus.Core.Data.Entities;
using MediBook.Backend.ViewModels.Shared;
using MediBook.Data.Entities;
using MediBook.Data.Entities.Filters;
using MediBook.Backend.ViewModels.Doctors;
using MediBook.Services.Abstractions;

namespace MediBook.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseSupervisorsPermission)]
  public class DoctorsController : ControllerBase
  {
    private IRepository<int, Doctor, DoctorFilter> DoctorRepository
    {
      get => this.Storage.GetRepository<int, Doctor, DoctorFilter>();
    }
    private IRepository<int, int, DoctorSpecialization, DoctorSpecializationFilter> DoctorSpecializationRepository
    {
      get => this.Storage.GetRepository<int, int, DoctorSpecialization, DoctorSpecializationFilter>();
    }

    private readonly IAuthService authService;

    public DoctorsController(IStorage storage, IAuthService authService)
      : base(storage)
    {
      this.authService = authService;
    }

    public async Task<IActionResult> Index(DoctorFilter filter = null, string sorting = "-created", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(await DoctorRepository.GetAllAsync(filter, sorting, offset, limit, new Inclusion<Doctor>[]
      {
        new Inclusion<Doctor>(d => d.Organization),
        new Inclusion<Doctor>("Specializations.Specialization")
      }),
        sorting, offset, limit, await DoctorRepository.CountAsync(filter)));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEdit(int? id)
    {
      Doctor doctor = id.HasValue
        ? await DoctorRepository.GetByIdAsync(id.Value, new Inclusion<Doctor>[]
        {
          new Inclusion<Doctor>(s => s.Organization),
          new Inclusion<Doctor>(s => s.Specializations)
        })
        : null;
      return this.View(await CreateOrEditViewModelFactory.CreateAsync(doctor, HttpContext));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Doctor doctor = createOrEdit.Id.HasValue ? await DoctorRepository.GetByIdAsync(createOrEdit.Id.Value) : new();

        bool containErrors = false;

        if (!createOrEdit.Id.HasValue && string.IsNullOrWhiteSpace(createOrEdit.Password))
        {
          TempData["Password"] = "Введіть пароль";
          containErrors = true;
        }
        if (createOrEdit.Id.HasValue && doctor.Email != createOrEdit.Identifier && await authService.EmailExists(createOrEdit.Identifier)
          || !createOrEdit.Id.HasValue && await authService.EmailExists(createOrEdit.Identifier))
        {
          TempData["Email"] = "Даний Email вже використовується";
          containErrors = true;
        }

        if (containErrors)
          return CreateRedirectToSelfResult();

        doctor.About = createOrEdit.About;
        doctor.FullName = createOrEdit.FullName;
        doctor.IsActivated = createOrEdit.IsActivated;
        doctor.IsHidden = createOrEdit.IsHidden;
        doctor.OrganizationId = createOrEdit.OrganizationId;
        doctor.PhotoUrl = createOrEdit.PhotoUrl;
        doctor.ProfessionalEducation = createOrEdit.ProfessionalEducation;
        doctor.ProfessionalExperience = createOrEdit.ProfessionalExperience;
        doctor.Email = createOrEdit.Identifier;
        doctor.Phone = createOrEdit.Phone;
        doctor.IsAvailableOnline = createOrEdit.IsAvailableOnline;
        doctor.Room = createOrEdit.Room;

        if (!string.IsNullOrWhiteSpace(createOrEdit.Password))
          authService.SetPassword(doctor, createOrEdit.Password);

        if (!createOrEdit.Id.HasValue)
        {
          doctor.Created = System.DateTime.Now;
          DoctorRepository.Create(doctor);
        }
        else
          DoctorRepository.Edit(doctor);

        await this.Storage.SaveAsync();
        await this.CreateOrEditDoctorSpecializationsAsync(doctor.Id, createOrEdit.DoctorSpecializations);
        return this.Redirect(HttpContext.Request.CombineUrl("/backend/doctors"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> Delete(int id)
    {
      Doctor doctor = await DoctorRepository.GetByIdAsync(id);

      if (doctor is null)
        return NotFound();

      DoctorRepository.Delete(doctor.Id);

      await this.Storage.SaveAsync();
      return this.RedirectToAction("Index");
    }

    private async Task CreateOrEditDoctorSpecializationsAsync(int doctorId, DoctorSpecializationViewModel[] doctorSpecializations)
    {
      await DeleteDoctorSpecializations(doctorId);
      CreateDoctorSpecializations(doctorId, doctorSpecializations);

      await this.Storage.SaveAsync();
    }

    private async Task DeleteDoctorSpecializations(int doctorId)
    {
      IEnumerable<DoctorSpecialization> doctorSpecializations = await DoctorSpecializationRepository.GetAllAsync(new(doctorId: doctorId));

      foreach (var doctorSpecialization in doctorSpecializations)
      {
        DoctorSpecializationRepository.Delete(doctorSpecialization.DoctorId, doctorSpecialization.SpecializationId);
      }
    }

    private void CreateDoctorSpecializations(int doctorId, DoctorSpecializationViewModel[] doctorSpecializations)
    {
      foreach (var doctorSpecialization in doctorSpecializations.Where(pc => pc.Selected))
      {
        DoctorSpecializationRepository.Create(new DoctorSpecialization
        {
          DoctorId = doctorId,
          SpecializationId = doctorSpecialization.SpecializationId
        });
      }
    }

  }
}