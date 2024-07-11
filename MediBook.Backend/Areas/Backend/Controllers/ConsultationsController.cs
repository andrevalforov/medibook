using DinkToPdf;
using DinkToPdf.Contracts;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediBook.Data.Entities;
using MediBook.Data.Entities.Filters;
using MediBook.Backend.ViewModels.Consultations;
using System.Threading.Tasks;

namespace MediBook.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseSupervisionsPermission)]
  public class ConsultationsController : ControllerBase
  {
    private readonly IConverter converter;

    private IRepository<int, Consultation, ConsultationFilter> ConsultationRepository
    {
      get => this.Storage.GetRepository<int, Consultation, ConsultationFilter>();
    }

    public ConsultationsController(IStorage storage, IConverter converter)
      : base(storage)
    {
      this.converter = converter;
    }

    public async Task<IActionResult> Index(ConsultationFilter filter = null, string sorting = "-scheduled", int offset = 0, int limit = 10)
    {
      return this.View(await IndexViewModelFactory.CreateAsync(await ConsultationRepository.GetAllAsync(filter, sorting, offset, limit, new Inclusion<Consultation>[]
      {
        new Inclusion<Consultation>(s => s.Doctor),
        new Inclusion<Consultation>(s => s.Patient)
      }), HttpContext, sorting, offset, limit, await ConsultationRepository.CountAsync(filter)));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
      return this.View(DetailsViewModelFactory.Create(await ConsultationRepository.GetByIdAsync(id, new Inclusion<Consultation>[]
      {
        new Inclusion<Consultation>("Doctor.Specializations.Specialization"),
        new Inclusion<Consultation>("Attachments.Doctor"),
        new Inclusion<Consultation>("Attachments.Patient"),
        new Inclusion<Consultation>(s => s.Patient)
      })));
    }

    //[AllowAnonymous]
    //[HttpGet]
    //public async Task<IActionResult> Preview(int id)
    //{
    //  Data.Entities.Supervision supervision = await SupervisionRepository.GetByIdAsync(id, new Inclusion<Data.Entities.Supervision>[]
    //  {
    //    new Inclusion<Data.Entities.Supervision>(s => s.Supervisor),
    //    new Inclusion<Data.Entities.Supervision>(s => s.Supervisee),
    //    new Inclusion<Data.Entities.Supervision>(s => s.Topic.NameCode.Localizations)
    //  });

    //  if (supervision is null)
    //    return this.NotFound();

    //  return this.View("SupervisionPdf", DetailsViewModelFactory.Create(supervision));
    //}

    //[HttpGet]
    //public async Task<IActionResult> Download(int id)
    //{
    //  Data.Entities.Supervision supervision = await SupervisionRepository.GetByIdAsync(id);

    //  if (supervision is null)
    //    return this.NotFound();

    //  HtmlToPdfDocument document = new HtmlToPdfDocument
    //  {
    //    GlobalSettings =
    //    {
    //      PaperSize = PaperKind.A4,
    //      Orientation = Orientation.Portrait,
    //      DocumentTitle = "Звіт по супервізії"
    //    },
    //    Objects =
    //    {
    //      new ObjectSettings
    //      {
    //        Page = $"{this.Request.Scheme}://{this.Request.Host}/backend/supervisions/preview/{id}"
    //      }
    //    }
    //  };

    //  byte[] pdf = this.converter.Convert(document);
    //  return File(pdf, "application/pdf", $"Звіт по супервізії Id - {id}.pdf");
    //}

    //public async Task<ActionResult> Delete(int id)
    //{
    //  Data.Entities.Supervision supervision = await SupervisionRepository.GetByIdAsync(id);

    //  if (supervision is null)
    //    return this.BadRequest();

    //  SupervisionRepository.Delete(supervision.Id);
    //  await this.Storage.SaveAsync();

    //  return this.RedirectToAction("Index");
    //}
  }
}