using Magicalizer.Data.Entities.Abstractions;
using System;

namespace MediBook.Data.Entities
{
  public class Email : IEntity
  {
    public int Id { get; set; }
    public string Receiver { get; set; }
    public string Subject { get; set; }
    public string Text { get; set; }
    public DateTime Created { get; set; }
    public DateTime TimeToSend { get; set; }
    public DateTime? Sent { get; set; }
    public DateTime? FailedToSend { get; set; }
  }
}
