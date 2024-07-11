using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using MediBook.Data.Entities;
using MediBook.Data.Entities.Filters;
using MediBook.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;

namespace MediBook.Services.Defaults
{
  public class AuthService : IAuthService
  {
    private readonly IStorage storage;
    private readonly IHttpContextAccessor httpContextAccessor;

    private IRepository<int, Patient, PatientFilter> PatientRepository
    {
      get => this.storage.GetRepository<int, Patient, PatientFilter>();
    }
    private IRepository<int, Doctor, DoctorFilter> DoctorRepository
    {
      get => this.storage.GetRepository<int, Doctor, DoctorFilter>();
    }
    private IRepository<Guid, RestorePasswordToken, IFilter> RestorePasswordTokenRepository
    {
      get => this.storage.GetRepository<Guid, RestorePasswordToken, IFilter>();
    }

    public AuthService(IStorage storage, IHttpContextAccessor httpContextAccessor)
    {
      this.storage = storage;
      this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<ValidationResult> ValidateAsync(string identifier, string secret)
    {
      if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(secret))
        return new ValidationResult { Success = false };

      Doctor doctor = (await DoctorRepository.GetAllAsync(new(email: new(equals: identifier.ToLowerInvariant())))).FirstOrDefault();
      Patient patient = (await PatientRepository.GetAllAsync(new(email: new(equals: identifier.ToLowerInvariant())))).FirstOrDefault();

      if (doctor is not null)
      {
        if (doctor.IsActivated == false)
          return new ValidationResult { Success = false };

        string hash = Pbkdf2Hasher.ComputeHash(secret, Convert.FromBase64String(doctor.Extra));

        if (!string.Equals(hash, doctor.Secret))
          return new ValidationResult { Success = false };

        return new ValidationResult { Success = true, Doctor = doctor };
      }
      else if (patient is not null)
      {
        if (patient.IsActivated == false)
          return new ValidationResult { Success = false };

        string hash = Pbkdf2Hasher.ComputeHash(secret, Convert.FromBase64String(patient.Extra));

        if (!string.Equals(hash, patient.Secret))
          return new ValidationResult { Success = false };

        return new ValidationResult { Success = true, Patient = patient };
      }

      return new ValidationResult { Success = false };
    }

    public async Task<Guid?> CreateRestorePasswordTokenAsync(string identifier)
    {
      Doctor doctor = (await DoctorRepository.GetAllAsync(new(email: new(equals: identifier.ToLowerInvariant())))).FirstOrDefault();
      Patient patient = (await PatientRepository.GetAllAsync(new(email: new(equals: identifier.ToLowerInvariant())))).FirstOrDefault();

      if (doctor is null && patient is null) return null;

      RestorePasswordToken token = new RestorePasswordToken
      {
        Id = Guid.NewGuid(),
        DoctorId = doctor?.Id,
        PatientId = patient?.Id,
        Created = DateTime.Now
      };

      RestorePasswordTokenRepository.Create(token);
      await this.storage.SaveAsync();

      return token.Id;
    }

    public async Task<RestorePasswordResult> RestorePassword(Guid code)
    {
      RestorePasswordToken token = await RestorePasswordTokenRepository.GetByIdAsync(code, inclusions: new Inclusion<RestorePasswordToken>[]
      {
        new Inclusion<RestorePasswordToken>(t => t.Doctor),
        new Inclusion<RestorePasswordToken>(t => t.Patient)
      });

      if (token is null || token.Used.HasValue) return null;

      string password = new Random().Next(10000000, 99999999).ToString();

      if (token.Doctor is not null)
        this.SetPassword(token.Doctor, password);
      else
        this.SetPassword(token.Patient, password);

      token.Used = DateTime.Now;
      this.storage.Save();

      return new RestorePasswordResult { Password = password, Identifier = token.Doctor?.Email ?? token.Patient.Email };
    }

    public async Task SignInAsync(Patient patient, Doctor doctor, bool isPersistent = false)
    {
      string authenticationScheme = FrontendCookieAuthenticationDefaults.AuthenticationScheme;
      ClaimsIdentity identity = new(this.GetClaims(patient, doctor), authenticationScheme);
      ClaimsPrincipal principal = new(identity);

      await this.httpContextAccessor.HttpContext.SignInAsync(
        authenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistent }
      );
    }

    public async Task SignOutAsync()
    {
      await this.httpContextAccessor.HttpContext.SignOutAsync(FrontendCookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task<bool> EmailExists(string email)
    {
      Doctor doctor = (await DoctorRepository.GetAllAsync(new(email: new(equals: email.ToLowerInvariant())))).FirstOrDefault();
      Patient patient = (await PatientRepository.GetAllAsync(new(email: new(equals: email.ToLowerInvariant())))).FirstOrDefault();

      if (doctor is not null || patient is not null)
        return true;

      return false;
    }

    public void SetPassword(Patient patient, string password)
    {
      byte[] salt = Pbkdf2Hasher.GenerateRandomSalt();

      patient.Secret = Pbkdf2Hasher.ComputeHash(password, salt);
      patient.Extra = Convert.ToBase64String(salt);
    }

    public void SetPassword(Doctor doctor, string password)
    {
      byte[] salt = Pbkdf2Hasher.GenerateRandomSalt();

      doctor.Secret = Pbkdf2Hasher.ComputeHash(password, salt);
      doctor.Extra = Convert.ToBase64String(salt);
    }

    private IEnumerable<Claim> GetClaims(Patient patient, Doctor doctor)
    {
      if (doctor != null)
        return new List<Claim>
      {
        new Claim(ClaimTypes.NameIdentifier, doctor.Id.ToString()),
        new Claim(ClaimTypes.Role, "Doctor"),
        new Claim(ClaimTypes.Email, doctor.Email)
      };
      else
        return new List<Claim>
      {
        new Claim(ClaimTypes.NameIdentifier, patient.Id.ToString()),
        new Claim(ClaimTypes.Role, "Patient"),
        new Claim(ClaimTypes.Email, patient.Email)
      };
    }
  }

  public static class Pbkdf2Hasher
  {
    public static string ComputeHash(string password, byte[] salt)
    {
      return Convert.ToBase64String(
        KeyDerivation.Pbkdf2(
          password: password,
          salt: salt,
          prf: KeyDerivationPrf.HMACSHA1,
          iterationCount: 10000,
          numBytesRequested: 256 / 8
        )
      );
    }

    public static byte[] GenerateRandomSalt()
    {
      byte[] salt = new byte[128 / 8];

      using (var rng = RandomNumberGenerator.Create())
        rng.GetBytes(salt);

      return salt;
    }
  }
}

