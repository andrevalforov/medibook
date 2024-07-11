using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediBook.Services.Abstractions
{
  public interface IEmailService
  {
    Task CreateEmailFromTemplateBySupervisor(int supervisorId, string templateCode, DateTime? timeToSend = null, IEnumerable<EmailParameter> @params = null);
    Task CreateEmailFromTemplateBySupervisee(int superviseeId, string templateCode, DateTime? timeToSend = null, IEnumerable<EmailParameter> @params = null);
    Task CreateEmailFromTemplate(string templateCode, string receiver, DateTime? timeToSend = null, IEnumerable<EmailParameter> @params = null);
    Task AddToQueue(int userId, string subject, string text, DateTime timeToSend);
    Task AddToQueue(string receiver, string subject, string text, DateTime timeToSend);
    Task CheckSupervisionCommentsAsync(int supervisionId, string templateCode);
    Task SendEmailToAdmins(string templateCode, IEnumerable<EmailParameter> @params = null);
    Task SendEmail();
    Task SendEmail(int userId, string subject, string body, IDictionary<string, byte[]> attachements);
  }
}
