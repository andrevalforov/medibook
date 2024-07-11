using System.Collections.Generic;
using MediBook.Backend.ViewModels.Shared;

namespace MediBook.Backend.ViewModels.Emails
{
  public class IndexViewModel : IndexViewModelBase
  {
    public IEnumerable<EmailViewModel> Emails { get; set; }
  }
}
