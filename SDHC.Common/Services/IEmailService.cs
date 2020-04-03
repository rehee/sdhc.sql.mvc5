using SDHC.Common.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Services
{
  public interface IEmailService
  {
    Task SendEmailAsync(string email, string subject, string message);
    string Host { get; set; }
    int Port { get; set; }
    string User { get; set; }
    string Password { get; set; }
    bool SSL { get; set; }
    void UpdateEmailSetting(string host, string port, string user, string password, string ssl);
    void SendEmail(string toUser, string title, string body, string fromUser);
  }

  public class EmailService : IEmailService
  {
    private SystemConfig config { get; }
    public EmailService(SystemConfig config)
    {
      this.config = config;
      this.Host = config.EmailHost;
      this.Port = config.EmailPort;
      this.User = config.EmailUser;
      this.Password = config.EmailPassword;
      this.SSL = config.EmailSSL;
    }
    public string Host { get; set; } = "smtpout.europe.secureserver.net";
    public int Port { get; set; } = 3535;
    public string User { get; set; } = "test@spxus.co.uk";
    public string Password { get; set; } = "email12345";
    public bool SSL { get; set; } = false;

    public void UpdateEmailSetting(string host, string port, string user, string password, string ssl)
    {
      if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
      {
        return;
      }
      Host = host.Text();
      Port = port.Text().MyTryConvert<int>();
      User = user.Text();
      Password = password.Text();
      SSL = ssl.Text().MyTryConvert<bool>();
    }

    public void SendEmail(string toUser, string title, string body, string fromUser)
    {
      var mailToArray = new List<string>() { toUser.Text() };
      var mailSubject = title.Text();
      var mailBody = body.Text();
      var isbodyHtml = true;
      MailAddress maddr = new MailAddress(User, fromUser);
      MailMessage myMail = new MailMessage();
      if (mailToArray != null)
      {
        for (int i = 0; i < mailToArray.Count; i++)
        {
          myMail.To.Add(mailToArray[i].ToString());
        }
      }
      myMail.Subject = mailSubject;
      myMail.From = maddr;
      myMail.Body = mailBody;
      myMail.BodyEncoding = Encoding.Default;
      myMail.Priority = MailPriority.High;
      myMail.IsBodyHtml = isbodyHtml;
      myMail.SubjectEncoding = System.Text.Encoding.UTF8;
      myMail.BodyEncoding = System.Text.Encoding.UTF8;
      SmtpClient smtp = GetSMTP();

      try
      {
        smtp.Send(myMail);
      }
      catch
      {
        try
        {
          smtp.Dispose();
        }
        catch { }
      }

    }
    private SmtpClient GetSMTP()
    {
      var smtp = new SmtpClient();
      smtp.EnableSsl = true;
      smtp.Host = Host;
      smtp.Port = Port;
      smtp.UseDefaultCredentials = false;
      smtp.Credentials = new System.Net.NetworkCredential(User, Password);
      smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
      smtp.EnableSsl = SSL;
      smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
      return smtp;
    }
    public Task SendEmailAsync(string email, string subject, string message)
    {
      return Task.Run(() =>
      {
        SendEmail(email, subject, message, User);
      });
    }
  }
}
