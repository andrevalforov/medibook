using MediBook.Data.Entities;

namespace MediBook.Backend.ViewModels.Emails
{
  public static class DetailsViewModelFactory
  {
    public static DetailsViewModel Create(Email email)
    {
      return new DetailsViewModel
      { 
        Id = email.Id,
        Receiver = email.Receiver,
        Sent = email.Sent,
        Subject = email.Subject,
        Text = email.Text
      };
    }
  }
}
