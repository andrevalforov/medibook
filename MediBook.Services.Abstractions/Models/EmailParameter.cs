using System;
namespace MediBook.Services.Abstractions
{
  public class EmailParameter
  {
    public string Code { get; set; }
    public string Value { get; set; }

    public EmailParameter() { }
    public EmailParameter(string code, string value)
    {
      this.Code = code;
      this.Value = value;
    }

  }
}

