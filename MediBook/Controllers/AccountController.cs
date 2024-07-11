using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using MediBook.Data.Entities;
using MediBook.Services.Abstractions;
using MediBook.ViewModels.Account;
using Platformus;
using Platformus.Core.Primitives;

namespace MediBook.Controllers
{
  public class AccountController : WebControllerBase
  {
    private readonly IAuthService authService;
    //private readonly IEmailService emailService;

    private IRepository<int, Patient, IFilter> PatientRepository
    {
      get => this.Storage.GetRepository<int, Patient, IFilter>();
    }
    private IRepository<Guid, RestorePasswordToken, IFilter> RestorePasswordTokenRepository
    {
      get => this.Storage.GetRepository<Guid, RestorePasswordToken, IFilter>();
    }
    private IRepository<int, CredentialType, CredentialTypeFilter> CredentialTypeRepository
    {
      get => this.Storage.GetRepository<int, CredentialType, CredentialTypeFilter>();
    }
    private IRepository<int, Credential, CredentialFilter> CredentialRepository
    {
      get => this.Storage.GetRepository<int, Credential, CredentialFilter>();
    }
    private IRepository<int, User, UserFilter> UserRepository
    {
      get => this.Storage.GetRepository<int, User, UserFilter>();
    }

    public AccountController(IStorage storage, IAuthService authService /*IEmailService emailService*/)
      : base(storage)
    {
      this.authService = authService;
      //this.emailService = emailService;
    }

    [HttpGet]
    [AllowAnonymous]
    [ImportModelStateFromTempData]
    public IActionResult SignIn()
    {
      return this.View("SignInPage");
    }

    [HttpPost]
    [AllowAnonymous]
    [ExportModelStateToTempData]
    public async Task<IActionResult> SignIn(SignInPageViewModel signInPageViewModel)
    {
      ValidationResult validateResult = await authService.ValidateAsync(signInPageViewModel.Email, signInPageViewModel.Password);

      if (validateResult.Success && !string.IsNullOrWhiteSpace(signInPageViewModel.Password))
      {
        await authService.SignInAsync(validateResult.Patient, validateResult.Doctor);
        return this.Redirect("/");
      }
      else
        this.ModelState.AddModelError(nameof(signInPageViewModel.Password), "Некоректна електронна пошта або пароль");

      return this.View("SignInPage", signInPageViewModel);
    }

    [HttpGet]
    [AllowAnonymous]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> SignUp()
    {
      return this.View("SignUpPage", new SignUpPageViewModel
      {
        GenderOptions = (Enum.GetValues(typeof(Gender)) as IEnumerable<Gender>)
          .Select(t => new Option(t.GetDisplayName(), ((int)t).ToString()))
      });
    }

    [HttpPost]
    [AllowAnonymous]
    [ExportModelStateToTempData]
    public async Task<IActionResult> SignUp(SignUpPageViewModel signUpPageViewModel)
    {
      if (this.ModelState.IsValid)
      {
        if (await authService.EmailExists(signUpPageViewModel.Email))
        {
          ModelState.AddModelError("Email", "Пошта вже використовується");
        }
        else
        {
          Patient patient = this.CreatePatient(signUpPageViewModel);
          authService.SetPassword(patient, signUpPageViewModel.Password);

          PatientRepository.Create(patient);
          await this.Storage.SaveAsync();

          await authService.SignInAsync(patient, null);
          return this.Redirect("/");
        }
      }

      return this.CreateRedirectToSelfResult();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GenerateResetPasswordToken()
    {
      return this.View("RestorePassword", new RestorePasswordViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateResetPasswordToken([FromForm] string email)
    {
      if (await authService.EmailExists(email))
      {
        var token = await authService.CreateRestorePasswordTokenAsync(email);
        string requestUrl = $"{this.Request.Scheme}://{this.Request.Host}/restore-password/confirm?code={token}";

        IEnumerable<EmailParameter> @params = new List<EmailParameter> { new EmailParameter("requestUrl", $"{requestUrl}") };
        //await this.emailService.CreateEmailFromTemplate("RestorePasswordRequest", email, null, @params);
      }

      return this.Redirect($"/restore-password-token-created");
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> RestorePassword(Guid code)
    {
      RestorePasswordResult result = await authService.RestorePassword(code);

      IEnumerable<EmailParameter> @params = new List<EmailParameter> { new EmailParameter("newPass", $"{result.Password}") };
      //await this.emailService.CreateEmailFromTemplate("PasswordRestored", email, null, @params);

      return this.Redirect($"/restore-password-token-activated");
    }

    [HttpGet]
    public async Task<IActionResult> SignOutAsync()
    {
      await authService.SignOutAsync();
      return this.Redirect("/");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult About()
    {
      return this.View();
    }

    private Patient CreatePatient(SignUpPageViewModel signUpPageViewModel)
    {
      Patient patient = new()
      {
        FirstName = signUpPageViewModel.FirstName,
        LastName = signUpPageViewModel.LastName,
        MiddleName = signUpPageViewModel.MiddleName ?? string.Empty,
        Birthday = signUpPageViewModel.Birthday,
        Email = signUpPageViewModel.Email,
        Gender = signUpPageViewModel.Gender,
        Phone = signUpPageViewModel.Phone,
        IsActivated = true,
        Created = DateTime.Now
      };

      return patient;
    }

    private async Task<string> GetEmail(int userId)
    {
      CredentialType credentialType = (await CredentialTypeRepository.GetAllAsync(new CredentialTypeFilter(code: "Email"))).FirstOrDefault();

      if (credentialType == null) return null;

      Credential credential = (await CredentialRepository.GetAllAsync(new CredentialFilter(user: new UserFilter(id: userId))))
        .FirstOrDefault(c => c.CredentialTypeId == credentialType.Id);

      return credential.Identifier;
    }

  }
}