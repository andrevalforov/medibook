using System;
using Platformus.Core.Backend.ViewModels;

namespace MediBook.Backend.ViewModels.Emails
{
  public class DetailsViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Receiver { get; set; }
    public string Subject { get; set; }
    public string Text { get; set; }
    public DateTime? Sent { get; set; }
  }
}
