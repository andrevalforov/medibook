using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus;
using MediBook.Backend.ViewModels.Organizations;

using MediBook.Data.Entities;
using MediBook.Data.Entities.Filters;

namespace MediBook.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseOrganizationsPermission)]
  public class OrganizationsController : ControllerBase
  {
    private IRepository<int, Organization, OrganizationFilter> OrganizationRepository
    {
      get => this.Storage.GetRepository<int, Organization, OrganizationFilter>();
    }

    public OrganizationsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> Index(OrganizationFilter filter = null, string sorting = "+name", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(await OrganizationRepository.GetAllAsync(filter, sorting, offset, limit,
        new Inclusion<Organization>(o => o.City)),
        sorting, offset, limit, await OrganizationRepository.CountAsync(filter)));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEdit(int? id)
    {
      Organization organization = id.HasValue
        ? await OrganizationRepository.GetByIdAsync(id.Value, new Inclusion<Organization>(o => o.City.Region))
        : null;

      return this.View(await CreateOrEditViewModelFactory.CreateAsync(organization, HttpContext));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Organization organization = createOrEdit.Id.HasValue ? await OrganizationRepository.GetByIdAsync(createOrEdit.Id.Value) : new Organization();

        organization.CityId = int.Parse(createOrEdit.CityValue);
        organization.Name = createOrEdit.Name;
        organization.Address = createOrEdit.Address;

        if (!createOrEdit.Id.HasValue)
          OrganizationRepository.Create(organization);
        else
          OrganizationRepository.Edit(organization);

        await this.Storage.SaveAsync();

        return this.Redirect(HttpContext.Request.CombineUrl("/backend/organizations"));
      }
      return this.CreateRedirectToSelfResult();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      Organization organization = await OrganizationRepository.GetByIdAsync(id);

      if (organization is null)
        return this.BadRequest();

      OrganizationRepository.Delete(organization.Id);
      await this.Storage.SaveAsync();

      return this.RedirectToAction("Index");
    }
  }
}
