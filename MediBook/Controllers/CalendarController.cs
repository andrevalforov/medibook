using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Services.Abstractions;
using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;
using MediBook.Data.Entities.Filters;
using System.Threading.Tasks;

namespace MediBook.Controllers
{
  [AllowAnonymous]
  public class CalendarController : WebControllerBase
  {
    private readonly IUserManager userManager;
    private IRepository<int, Patient, PatientFilter> PatientRepository
    {
      get => this.Storage.GetRepository<int, Patient, PatientFilter>();
    }
    private IRepository<int, Doctor, DoctorFilter> DoctorRepository
    {
      get => this.Storage.GetRepository<int, Doctor, DoctorFilter>();
    }

    public CalendarController(IStorage storage, IUserManager userManager)
      : base(storage)
    {
      this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> ForDoctor(int id, int year, int month)
    {
      Doctor doctor = await DoctorRepository.GetByIdAsync(id);

      if (!this.AreParamsValid(year, month))
        return this.BadRequest();

      return this.PartialView("_Calendar", CalendarViewModelFactory.Create(HttpContext, doctor, year, month));
    }

    [HttpGet]
    public async Task<IActionResult> ForPatient(int id, int year, int month)
    {
      Patient patient = await PatientRepository.GetByIdAsync(id);

      if (!this.AreParamsValid(year, month))
        return this.BadRequest();

      return this.PartialView("_Calendar", CalendarViewModelFactory.Create(HttpContext, patient, year, month));
    }

    private bool AreParamsValid(int year, int month)
    {
      return year > 0 && month > 0 && month <= 12;
    }
  }
}
