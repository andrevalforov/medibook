using System;
using MediBook.Data.Entities;

namespace MediBook.Services.Abstractions
{
  public class ValidationResult
  {
    public bool Success { get; set; }
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
  }
}

