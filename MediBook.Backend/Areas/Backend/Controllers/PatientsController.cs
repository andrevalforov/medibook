using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus;
using MediBook.Data.Entities;
using MediBook.Data.Entities.Filters;
using MediBook.Backend.ViewModels.Patients;
using MediBook.Services.Abstractions;

namespace MediBook.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseSuperviseesPermission)]
  public class PatientsController : ControllerBase
  {
    private IRepository<int, Patient, PatientFilter> PatientRepository
    {
      get => this.Storage.GetRepository<int, Patient, PatientFilter>();
    }

    private readonly IAuthService authService;

    public PatientsController(IStorage storage, IAuthService authService)
      : base(storage)
    {
      this.authService = authService;
    }

    public async Task<IActionResult> Index(PatientFilter filter = null, string sorting = "-created", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(await PatientRepository.GetAllAsync(filter, sorting, offset, limit),
        sorting, offset, limit, await PatientRepository.CountAsync(filter)));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEdit(int? id)
    {
      Patient patient = id.HasValue
        ? await PatientRepository.GetByIdAsync(id.Value)
        : null;

      return this.View(CreateOrEditViewModelFactory.Create(patient));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Patient patient = createOrEdit.Id.HasValue ? await PatientRepository.GetByIdAsync(createOrEdit.Id.Value) : new();

        bool containErrors = false;

        if (!createOrEdit.Id.HasValue && string.IsNullOrWhiteSpace(createOrEdit.Password))
        {
          TempData["Password"] = "Введіть пароль";
          containErrors = true;
        }
        if (createOrEdit.Id.HasValue && patient.Email != createOrEdit.Email && await authService.EmailExists(createOrEdit.Email)
          || !createOrEdit.Id.HasValue && await authService.EmailExists(createOrEdit.Email))
        {
          TempData["Email"] = "Даний Email вже використовується";
          containErrors = true;
        }

        if (containErrors)
          return CreateRedirectToSelfResult();

        patient.About = createOrEdit.About;
        patient.Birthday = createOrEdit.Birthday;
        patient.Email = createOrEdit.Email;
        patient.FirstName = createOrEdit.FirstName;
        patient.Gender = (Gender)createOrEdit.Gender;
        patient.IsActivated = createOrEdit.IsActivated;
        patient.LastName = createOrEdit.LastName;
        patient.MiddleName = createOrEdit.MiddleName;
        patient.Phone = createOrEdit.Phone;

        if (!string.IsNullOrWhiteSpace(createOrEdit.Password))
          authService.SetPassword(patient, createOrEdit.Password);

        if (!createOrEdit.Id.HasValue)
        {
          patient.Created = System.DateTime.Now;
          PatientRepository.Create(patient);
        }
        else
          PatientRepository.Edit(patient);

        await this.Storage.SaveAsync();

        return this.Redirect(HttpContext.Request.CombineUrl("/backend/patients"));
      }
      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> Delete(int id)
    {
      Patient patient = await PatientRepository.GetByIdAsync(id);

      if (patient is null)
        return NotFound();

      PatientRepository.Delete(patient.Id);

      await this.Storage.SaveAsync();

      return this.RedirectToAction("Index");
    }
  }
}
