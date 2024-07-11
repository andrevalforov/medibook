using System;
namespace MediBook.Backend.ViewModels.Shared
{
	public class AttachmentViewModel
	{
		public int Id { get; set; }
		public string Sender { get; set; }
		public string FileUrl { get; set; }
		public string Comment { get; set; }
    public string Created { get; set; }
	}
}

