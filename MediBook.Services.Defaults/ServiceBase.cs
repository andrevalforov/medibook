using Magicalizer.Data.Repositories.Abstractions;

namespace MediBook.Services.Defaults
{
  public abstract class ServiceBase
  {
    protected readonly IStorage storage;

    public ServiceBase(IStorage storage)
    {
      this.storage = storage;
    }
  }
}
