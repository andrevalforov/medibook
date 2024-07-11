﻿using System.ComponentModel.DataAnnotations;

namespace MediBook.Attributes
{
  public class MustBeTrueAttribute : ValidationAttribute
  {
    public override bool IsValid(object value)
    {
      return value is bool && (bool)value;
    }
  }
}
