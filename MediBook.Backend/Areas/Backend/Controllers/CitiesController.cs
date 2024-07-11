using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus;
using MediBook.Backend.ViewModels.Cities;
using MediBook.Data.Entities;
using MediBook.Data.Entities.Filters;
using System.Threading.Tasks;

namespace MediBook.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseCitiesPermission)]
  public class CitiesController : ControllerBase
  {
    private IRepository<int, City, CityFilter> CityRepository
    {
      get => this.Storage.GetRepository<int, City, CityFilter>();
    }

    public CitiesController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> Index(CityFilter filter = null, string sorting = "+name", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(await CityRepository.GetAllAsync(filter, sorting, offset, limit, new Inclusion<City>(c => c.Region)),
        HttpContext, sorting, offset, limit, await CityRepository.CountAsync(filter)));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEdit(int? id)
    {
      City city = id.HasValue
        ? await CityRepository.GetByIdAsync(id.Value)
        : null;

      return this.View(CreateOrEditViewModelFactory.Create(city, HttpContext));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        City city = createOrEdit.Id.HasValue ? await CityRepository.GetByIdAsync(createOrEdit.Id.Value) : new City();

        if (!string.IsNullOrEmpty(createOrEdit.RegionValue))
          city.RegionId = int.Parse(createOrEdit.RegionValue);

        city.Name = createOrEdit.Name;

        if (!createOrEdit.Id.HasValue)
          CityRepository.Create(city);
        else
          CityRepository.Edit(city);

        await this.Storage.SaveAsync();
        return this.Redirect(HttpContext.Request.CombineUrl("/backend/cities"));
      }

      return this.CreateRedirectToSelfResult();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      City city = await CityRepository.GetByIdAsync(id);

      if (city is null)
        return this.BadRequest();

      CityRepository.Delete(city.Id);
      await this.Storage.SaveAsync();

      return this.RedirectToAction("Index");
    }
  }
}
