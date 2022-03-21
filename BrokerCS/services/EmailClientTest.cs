using System;

namespace BrokerCS.services {
  public class EmailClientTest: IEmailClient {
    private string fromEmail;

    public EmailClientTest(string fromEmail) {
      this.fromEmail = fromEmail;
    }

    public void SendEmail(string title, string text, string toEmail) {
      Console.WriteLine("=====================================");
      Console.WriteLine("'Enviando' um email de Teste");
      Console.WriteLine("De: " + fromEmail);
      Console.WriteLine("Para: " + toEmail);
      Console.WriteLine("Titulo: " + title);
      Console.WriteLine("Texto: " + text);
      Console.WriteLine("=====================================");
    }
  }
}