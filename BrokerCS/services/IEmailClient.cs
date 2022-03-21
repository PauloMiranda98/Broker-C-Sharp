using System;

namespace BrokerCS.services {
  public interface IEmailClient {
    void SendEmail(string title, string text, string toEmail);
  }
}