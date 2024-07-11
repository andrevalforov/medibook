using Microsoft.AspNetCore.Http;
using Platformus;
using MediBook.Data.Entities;
using MediBook.ViewModels.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediBook.ViewModels.Account
{
  public static class SignUpPageViewModelFactory
  {
    public static async Task<SignUpPageViewModel> Create(HttpContext httpContext)
    {
      return new SignUpPageViewModel
      {

      };
    }
  }
}
