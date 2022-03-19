using System;

namespace BrokerCS {
  public class EmailClient {
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

    public void SendEmail(string title, string text, string toEmail) {
      Console.WriteLine("=====================================");
      Console.WriteLine("Enviando um email...");
      Console.WriteLine("De: " + fromEmail);
      Console.WriteLine("Para: " + toEmail);
      Console.WriteLine("Titulo: " + title);
      Console.WriteLine("Texto: " + text);
      Console.WriteLine("=====================================");
    }
  }
}