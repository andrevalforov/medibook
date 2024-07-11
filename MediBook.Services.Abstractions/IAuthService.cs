using System;
using System.Threading.Tasks;
using MediBook.Data.Entities;

namespace MediBook.Services.Abstractions
{
	public interface IAuthService
	{
    Task<ValidationResult> ValidateAsync(string identifier, string secret);
    Task<Guid?> CreateRestorePasswordTokenAsync(string identifier);
    Task<RestorePasswordResult> RestorePassword(Guid code);
    Task SignInAsync(Patient patient, Doctor doctor, bool isPersistent = false);
    Task SignOutAsync();
    Task<bool> EmailExists(string email);
    void SetPassword(Patient patient, string password);
    void SetPassword(Doctor doctor, string password);
  }
}

