using System.Collections.Generic;

namespace MediBook.ViewModels.Shared
{
  public class ConsultationsScheduleGroupViewModel
  {
    public string Code { get; set; }
    public bool ShowPagination { get; set; }
    public IEnumerable<ConsultationsScheduleViewModel> Consultations { get; set; }
  }
}
