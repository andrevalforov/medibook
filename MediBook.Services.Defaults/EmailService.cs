//using Magicalizer.Data.Repositories.Abstractions;
//using Magicalizer.Filters.Abstractions;
//using Microsoft.AspNetCore.Hosting;
//using Platformus;
//using Platformus.Core.Data.Entities;
//using Platformus.Core.Filters;
//using Platformus.Core.Services.Abstractions;
//using Platformus.Website.Data.Entities;
//using Platformus.Website.Filters;
//using MediBook.Data.Abstractions;
//using MediBook.Data.Entities;
//using MediBook.Data.Entities.Filters;
//using MediBook.Services.Abstractions;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Threading.Tasks;
//using Object = Platformus.Website.Data.Entities.Object;

//namespace MediBook.Services.Defaults
//{
//  public class EmailService : ServiceBase, IEmailService
//  {
//    private readonly IEmailSender emailSender;
//    private readonly IEmailRepository emailRepository;
//    private readonly ICultureManager cultureManager;
//    private readonly IHostingEnvironment hostingEnvironment;
//    private readonly string adminsEmails;

//    private IRepository<int, CredentialType, CredentialTypeFilter> CredentialTypeRepository
//    {
//      get => this.storage.GetRepository<int, CredentialType, CredentialTypeFilter>();
//    }
//    private IRepository<int, Credential, CredentialFilter> CredentialRepository
//    {
//      get => this.storage.GetRepository<int, Credential, CredentialFilter>();
//    }
//    private IRepository<int, Class, ClassFilter> ClassRepository
//    {
//      get => this.storage.GetRepository<int, Class, ClassFilter>();
//    }
//    private IRepository<int, EmailTemplate, EmailTemplateFilter> EmailTemplateRepository
//    {
//      get => this.storage.GetRepository<int, EmailTemplate, EmailTemplateFilter>();
//    }
//    private IRepository<int, Doctor, SupervisorFilter> SupervisorRepository
//    {
//      get => this.storage.GetRepository<int, Doctor, SupervisorFilter>();
//    }
//    private IRepository<int, Supervisee, SuperviseeFilter> SuperviseeRepository
//    {
//      get => this.storage.GetRepository<int, Supervisee, SuperviseeFilter>();
//    }
//    private IRepository<int, Object, ObjectFilter> ObjectRepository
//    {
//      get => this.storage.GetRepository<int, Object, ObjectFilter>();
//    }

//    public EmailService(IStorage storage, IEmailSender emailSender, IConfigurationManager configurationManager, ICultureManager cultureManager, IHostingEnvironment hostingEnvironment)
//      : base(storage)
//    {
//      this.emailSender = emailSender;
//      this.emailRepository = this.storage.GetRepository<IEmailRepository>();
//      this.cultureManager = cultureManager;
//      this.hostingEnvironment = hostingEnvironment;
//      this.adminsEmails = configurationManager["Supervision", "Emails"];
//    }

//    public async Task CreateEmailFromTemplateBySupervisor(int supervisorId, string templateCode, DateTime? timeToSend = null, IEnumerable<EmailParameter> @params = null)
//    {
//      Doctor supervisor = await SupervisorRepository.GetByIdAsync(supervisorId);

//      if (supervisor == null) return;

//      string email = await this.GetEmailAsync(supervisor.UserId);

//      if (string.IsNullOrWhiteSpace(email)) return;

//      await this.CreateEmailFromTemplate(templateCode, email, timeToSend, @params);
//    }

//    public async Task CreateEmailFromTemplateBySupervisee(int superviseeId, string templateCode, DateTime? timeToSend = null, IEnumerable<EmailParameter> @params = null)
//    {
//      Supervisee supervisee = await SuperviseeRepository.GetByIdAsync(superviseeId);

//      if (supervisee == null) return;

//      string email = await this.GetEmailAsync(supervisee.UserId);

//      if (string.IsNullOrWhiteSpace(email)) return;

//      await this.CreateEmailFromTemplate(templateCode, email, timeToSend, @params);
//    }

//    public async Task CreateEmailFromTemplate(string templateCode, string receiver, DateTime? timeToSend = null, IEnumerable<EmailParameter> parameters = null)
//    {
//      (string text, string subject) = await GetTemplateTextAndSubjectAsync(templateCode, parameters);

//      await this.AddToQueue(receiver, subject, text, timeToSend ?? DateTime.Now);
//    }

//    public async Task AddToQueue(string receiver, string subject, string text, DateTime timeToSend)
//    {
//      this.emailRepository.Create(new Email
//      {
//        Receiver = receiver,
//        Subject = subject,
//        Text = text,
//        TimeToSend = timeToSend,
//        Created = DateTime.Now
//      });
//      await this.storage.SaveAsync();
//    }

//    public async Task AddToQueue(int userId, string subject, string text, DateTime timeToSend)
//    {
//      string email = await this.GetEmailAsync(userId);

//      if (string.IsNullOrWhiteSpace(email)) return;

//      this.emailRepository.Create(new Email
//      {
//        Receiver = email,
//        Subject = subject,
//        Text = text,
//        TimeToSend = timeToSend,
//        Created = DateTime.Now
//      });
//      await this.storage.SaveAsync();
//    }

//    public async Task CheckSupervisionCommentsAsync(int supervisionId, string templateCode)
//    {
//      Data.Entities.Supervision supervision = await this.storage.GetRepository<ISupervisionRepository>().WithKeyAsync(supervisionId);

//      if (supervision == null || supervision.Status == SupervisionStatus.Completed)
//        return;

//      if (string.IsNullOrWhiteSpace(supervision.SuperviseeComment))
//        await this.CreateEmailFromTemplateBySupervisee(supervision.SuperviseeId.Value, $"{templateCode}Supervisee");

//      if (string.IsNullOrWhiteSpace(supervision.SupervisionResults))
//        await this.CreateEmailFromTemplateBySupervisor(supervision.SupervisorId, $"{templateCode}Supervisor");
//    }

//    public async Task SendEmailToAdmins(string templateCode, IEnumerable<EmailParameter> @params = null)
//    {
//      if (string.IsNullOrWhiteSpace(this.adminsEmails))
//        return;

//      foreach (string email in adminsEmails.Split(','))
//        await this.CreateEmailFromTemplate(templateCode, email, null, @params);
//    }

//    public async Task SendEmail()
//    {
//      if (hostingEnvironment.IsDevelopment())
//        return;

//      IEnumerable<Email> emails = this.emailRepository.GetAvailable();

//      foreach (Email email in emails)
//      {
//        try
//        {
//          await this.emailSender.SendEmailAsync(email.Receiver, email.Subject, email.Text, null);
//        }
//        catch { email.FailedToSend = DateTime.Now; };

//        email.Sent = DateTime.Now;
//      }
//      this.storage.Save();
//    }

//    public async Task SendEmail(int userId, string subject, string body, IDictionary<string, byte[]> attachements)
//    {
//      await this.emailSender.SendEmailAsync(await this.GetEmailAsync(userId), subject, body, attachements);
//    }

//    private async Task<string> GetEmailAsync(int userId)
//    {
//      CredentialType credentialType = (await CredentialTypeRepository.GetAllAsync(new CredentialTypeFilter(code: "Email"))).FirstOrDefault();

//      if (credentialType == null) return null;

//      Credential credential = (await CredentialRepository.GetAllAsync(new CredentialFilter(user: new UserFilter(id: userId))))
//        .FirstOrDefault(c => c.CredentialTypeId == credentialType.Id);

//      return credential.Identifier;
//    }

//    private async Task<(string, string)> GetTemplateTextAndSubjectAsync(string code, IEnumerable<EmailParameter> parameters)
//    {
//      Object templateObject = (await ObjectRepository.GetAllAsync(new ObjectFilter(@class: new ClassFilter(code: "EmailTemplate")), inclusions: new Inclusion<Object>[]
//      {
//        new Inclusion<Object>("Properties.Member"),
//        new Inclusion<Object>("Properties.StringValue.Localizations")
//      })).FirstOrDefault(o => o.Properties.FirstOrDefault(p => p.Member.Code.Equals("Code") && p.StringValue.GetLocalizationValue().Equals(code)) is not null);

//      EmailTemplate template = new EmailTemplate
//      {
//        Code = templateObject.Properties.FirstOrDefault(p => p.Member.Code.Equals("Code")).StringValue.GetLocalizationValue(),
//        Subject = templateObject.Properties.FirstOrDefault(p => p.Member.Code.Equals("Subject")).StringValue,
//        Text = templateObject.Properties.FirstOrDefault(p => p.Member.Code.Equals("Body")).StringValue
//      };

//      if (template is null) return (string.Empty, string.Empty);

//      string text = template.Text.GetLocalizationValue(), subject = template.Subject.GetLocalizationValue();

//      foreach (var parameter in parameters ?? new List<EmailParameter>())
//        if (text.Contains($"%{parameter.Code}%"))
//          text = text.Replace($"%{parameter.Code}%", parameter.Value);

//      foreach (var parameter in parameters ?? new List<EmailParameter>())
//        if (subject.Contains($"%{parameter.Code}%"))
//          subject = subject.Replace($"%{parameter.Code}%", parameter.Value);

//      text = $"<p>{text.Replace(Environment.NewLine, "</p><p>")}</p>";
//      return (text, subject);
//    }
//  }
//}
