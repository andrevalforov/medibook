using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus;
using MediBook.Backend.ViewModels.Regions;
using MediBook.Data.Entities;

namespace MediBook.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseRegionsPermission)]
  public class RegionsController : ControllerBase
  {
    private IRepository<int, Region, IFilter> RegionRepository
    {
      get => this.Storage.GetRepository<int, Region, IFilter>();
    }

    public RegionsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> Index(string sorting = "+name", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(await RegionRepository.GetAllAsync(null, sorting, offset, limit),
        sorting, offset, limit, await RegionRepository.CountAsync()));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEdit(int? id)
    {
      Region region = id.HasValue
        ? await RegionRepository.GetByIdAsync(id.Value)
        : null;

      return this.View(CreateOrEditViewModelFactory.Create(region));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Region region = createOrEdit.Id.HasValue ? await RegionRepository.GetByIdAsync(createOrEdit.Id.Value) : new Region();
        region.Name = createOrEdit.Name;

        if (!createOrEdit.Id.HasValue)
          RegionRepository.Create(region);
        else
          RegionRepository.Edit(region);

        await this.Storage.SaveAsync();
        return this.Redirect(HttpContext.Request.CombineUrl("/backend/regions"));
      }
      return this.CreateRedirectToSelfResult();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      Region region = await RegionRepository.GetByIdAsync(id);

      if (region is null)
        return NotFound();

      RegionRepository.Delete(region.Id);
      await this.Storage.SaveAsync();

      return this.Redirect(HttpContext.Request.CombineUrl("/backend/regions"));
    }
  }
}
