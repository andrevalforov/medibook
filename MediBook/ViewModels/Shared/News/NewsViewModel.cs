using System.Dynamic;

namespace MediBook.ViewModels
{
  public class NewsViewModel
  {
    public int Id { get; set; }
    public ExpandoObject Data { get; set; }
  }
}
