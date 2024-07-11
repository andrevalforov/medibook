using System;
namespace MediBook.ViewModels.Shared
{
  public class AttachmentViewModel
  {
    public string Text { get; set; }
    public string File { get; set; }
    public string Author { get; set; }
    public int? DoctorId { get; set; }
    public int? PatientId { get; set; }
    public DateTime Created { get; set; }
  }
}

