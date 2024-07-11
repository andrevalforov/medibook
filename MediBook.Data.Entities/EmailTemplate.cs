using System;
using Magicalizer.Data.Entities.Abstractions;
using Platformus.Core.Data.Entities;

namespace MediBook.Data.Entities
{
  public class EmailTemplate : IEntity
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public int SubjectId { get; set; }
    public int TextId { get; set; }

    public virtual Dictionary Subject { get; set; }
    public virtual Dictionary Text { get; set; }
  }
}

