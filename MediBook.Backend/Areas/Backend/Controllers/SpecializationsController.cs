using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus;
using MediBook.Data.Entities;
using MediBook.Backend.ViewModels.Specializations;

namespace MediBook.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseTopicsPermission)]
  public class SpecializationsController : ControllerBase
  {
    private IRepository<int, Specialization, IFilter> SpecializationRepository
    {
      get => this.Storage.GetRepository<int, Specialization, IFilter>();
    }

    public SpecializationsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> Index(string sorting = "+position", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(await SpecializationRepository.GetAllAsync(null, sorting, offset, limit),
        sorting, offset, limit, await SpecializationRepository.CountAsync()));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEdit(int? id)
    {
      Specialization specialization = id.HasValue
        ? await SpecializationRepository.GetByIdAsync(id.Value)
        : null;
      return this.View(CreateOrEditViewModelFactory.Create(specialization));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Specialization specialization = createOrEdit.Id.HasValue ? await SpecializationRepository.GetByIdAsync(createOrEdit.Id.Value) : new();

        specialization.Name = createOrEdit.Name;
        specialization.Position = createOrEdit.Position;

        if (!createOrEdit.Id.HasValue)
          SpecializationRepository.Create(specialization);
        else
          SpecializationRepository.Edit(specialization);

        await this.Storage.SaveAsync();
        return this.Redirect(HttpContext.Request.CombineUrl("/backend/specializations"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> Delete(int id)
    {
      Specialization specialization = await SpecializationRepository.GetByIdAsync(id);

      if (specialization is null)
        return this.BadRequest();

      SpecializationRepository.Delete(specialization.Id);
      await this.Storage.SaveAsync();

      return this.RedirectToAction("Index");
    }
  }
}