using System;
using Platformus.Core.Backend.ViewModels;

namespace MediBook.Backend.ViewModels.Shared
{
  public class EmailViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Receiver { get; set; }
    public string Subject { get; set; }
    public DateTime? Sent { get; set; }
  }
}
