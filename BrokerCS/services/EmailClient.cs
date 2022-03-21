using System;
using BrokerCS.services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BrokerCS {
  public class EmailClient : IEmailClient {
    private string smtpDomain;
    private string smtpPort;
    private string smtpUsername;
    private string smtpPassword;
    private string fromEmail;

    public EmailClient(string smtpDomain, string smtpPort, string smtpUsername, string smtpPassword, string fromEmail) {
      this.smtpDomain = smtpDomain;
      this.smtpPort = smtpPort;
      this.smtpUsername = smtpUsername;
      this.smtpPassword = smtpPassword;
      this.fromEmail = fromEmail;
    }

    public async void SendEmail(string title, string text, string toEmail) {
      var sendGridClient = new SendGridClient(smtpPassword);
      var from = new EmailAddress(fromEmail, "Broker");
      var subject = title;
      var to = new EmailAddress(toEmail);
      var plainContent = text;
      var htmlContent = text;

      Console.WriteLine("=====================================");
      Console.WriteLine("Enviando um email usando SendGrid");
      Console.WriteLine("De: " + fromEmail);
      Console.WriteLine("Para: " + toEmail);
      Console.WriteLine("Titulo: " + title);
      Console.WriteLine("Texto: " + text);
      Console.WriteLine("=====================================");

      var mailMessage = MailHelper.CreateSingleEmail(from, to, subject, plainContent, htmlContent);
      await sendGridClient.SendEmailAsync(mailMessage);
    }
  }
}