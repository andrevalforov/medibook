//using DinkToPdf;
//using DinkToPdf.Contracts;
//using Magicalizer.Data.Repositories.Abstractions;
//using Magicalizer.Filters.Abstractions;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using MediBook.Backend.ViewModels.Statistics;
//using System;
//using System.Threading.Tasks;

//namespace MediBook.Backend.Controllers
//{
//	[Area("Backend")]
//	[Authorize(Policy = Policies.HasBrowseStatisticsPermission)]
//	public class StatisticsController : ControllerBase
//	{
//		private readonly IConverter converter;

//		public StatisticsController(IStorage storage, IConverter converter)
//			: base(storage)
//		{
//			this.converter = converter;
//		}

//    [HttpGet]
//    public async Task<IActionResult> Index(DateTimeFilter filter = null)
//		{
//      DateTime createdFrom = filter?.From ?? DateTime.Now.AddDays(-7);
//      DateTime createdTo = filter?.To ?? DateTime.Now;

//			if (filter?.From is null)
//        this.Request.QueryString = this.Request.QueryString.Add("from", createdFrom.ToString("s"));
//      if (filter?.To is null)
//        this.Request.QueryString = this.Request.QueryString.Add("to", createdTo.ToString("s"));

//      return this.View(await IndexViewModelFactory.Create(HttpContext, createdFrom, createdTo));
//		}

//		[AllowAnonymous]
//		[HttpGet]
//		public async Task<IActionResult> Preview(DateTimeFilter filter = null)
//		{
//			DateTime createdFrom = filter?.From ?? DateTime.Now.AddDays(-7);
//			DateTime createdTo = filter?.To ?? DateTime.Now;
			
//			return this.View("StatisticsPdf", await IndexViewModelFactory.Create(HttpContext, createdFrom, createdTo));
//		}

//		[HttpGet]
//		public IActionResult Download(DateTimeFilter filter = null)
//		{
//      DateTime createdFrom = filter?.From ?? DateTime.Now.AddDays(-7);
//      DateTime createdTo = filter?.To ?? DateTime.Now;

//      HtmlToPdfDocument document = new HtmlToPdfDocument
//			{
//				GlobalSettings =
//				{
//					PaperSize = PaperKind.A4,
//					Orientation = Orientation.Portrait,
//					DocumentTitle = "Статистика"
//				},
//				Objects =
//				{
//					new ObjectSettings
//					{
//						Page = $"{this.Request.Scheme}://{this.Request.Host}/backend/statistics/preview?from={createdFrom.ToString("s")}&to={createdTo.ToString("s")}"
//					}
//				}
//			};
//			byte[] pdf = this.converter.Convert(document);
//			return File(pdf, "application/pdf", $"Статистика.pdf");
//		}
//	}
//}