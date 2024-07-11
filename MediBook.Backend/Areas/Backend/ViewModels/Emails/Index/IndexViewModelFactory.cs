using MediBook.Backend.ViewModels.Shared;

using MediBook.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MediBook.Backend.ViewModels.Emails
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(IEnumerable<Email> emails, string sorting, int offset, int limit, int total)
    {
      return new IndexViewModel()
      {
        Emails = emails.Select(EmailViewModelFactory.Create),
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total
      };
    }
  }
}
