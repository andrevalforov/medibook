namespace MediBook.Backend.ViewModels
{
  public abstract class IndexViewModelBase
  {
    public string Sorting { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
    public int Total { get; set; }
  }
}