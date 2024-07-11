using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediBook.Backend.ViewModels.Emails;
using MediBook.Data.Entities;

namespace MediBook.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseEmailsPermission)]
  public class EmailsController : ControllerBase
  {
    private IRepository<int, Email, IFilter> EmailRepository
    {
      get => this.Storage.GetRepository<int, Email, IFilter>();
    }

    public EmailsController(IStorage storage)
      : base(storage)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Index( string sorting = "-sent", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(await EmailRepository.GetAllAsync(null, sorting, offset, limit),
        sorting, offset, limit, await EmailRepository.CountAsync()));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
      Email email = await EmailRepository.GetByIdAsync(id);

      if (email?.Sent.HasValue != true)
        return this.BadRequest("Email not found");

      return this.View(DetailsViewModelFactory.Create(email));
    }
  }
}
