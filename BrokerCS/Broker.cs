using System;
using System.Threading;

namespace BrokerCS {
  public class Broker {
    private string toEmail;
    private AssetWatcher assetWatcher;
    private EmailClient emailClient;

    public Broker(AssetWatcher assetWatcher, EmailClient emailClient, string toEmail) {
      this.assetWatcher = assetWatcher;
      this.emailClient = emailClient;
      this.toEmail = toEmail;
    }

    public void Start() {
      int lastEmailType = -1;

      while (true) {
        if (assetWatcher.ShouldBuy()) {
          if (lastEmailType != 0) {
            SendEmailToBuy();
            lastEmailType = 0;
          }
        }
        else if (assetWatcher.ShouldSell()) {
          if (lastEmailType != 1) {
            SendEmailToSell();
            lastEmailType = 1;
          }
        }
        else {
          lastEmailType = -1;
        }

        Thread.Sleep(2000);
      }
    }

    void SendEmailToBuy() {
      string title = "Compre ações do ativo " + assetWatcher.assetName;
      string text = "O valor da ação " + assetWatcher.assetName + " ficou abaixo de " + assetWatcher.valueToBuy +
                    ". Aproveite essa oportunidade para comprar!";

      emailClient.SendEmail(title, text, toEmail);
    }

    void SendEmailToSell() {
      string title = "Venda ações do ativo " + assetWatcher.assetName;
      string text = "O valor da ação " + assetWatcher.assetName + " ficou acima de " + assetWatcher.valueToSell +
                    ". Aproveite essa oportunidade para vender!";

      emailClient.SendEmail(title, text, toEmail);
    }
  }
}