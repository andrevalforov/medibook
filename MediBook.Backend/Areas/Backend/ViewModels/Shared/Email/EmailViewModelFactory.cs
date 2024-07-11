using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Shared
{
  public static class EmailViewModelFactory
  {
    public static EmailViewModel Create(Email email)
    {
      return new EmailViewModel
      {
        Id = email.Id,
        Receiver = email.Receiver,
        Subject = email.Subject,
        Sent = email.Sent
      };
    }
  }
}
