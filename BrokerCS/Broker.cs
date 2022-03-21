using System.Threading;
using BrokerCS.services;

namespace BrokerCS {
  public class Broker {
    private string toEmail;
    private string assetName;
    private double valueToSell;
    private double valueToBuy;
    private IAssetClient assetClient;
    private IEmailClient emailClient;
    private int lastEmailType = -1;

    public Broker(string assetName, double valueToSell, double valueToBuy, string toEmail, IAssetClient assetClient, IEmailClient emailClient) {
      this.assetName = assetName;
      this.valueToSell = valueToSell;
      this.valueToBuy = valueToBuy;
      this.assetClient = assetClient;
      this.emailClient = emailClient;
      this.toEmail = toEmail;
    }

    public void Start() {
      while (true) {
        VerifyAsset();
        Thread.Sleep(10000);
      }
    }
    async void VerifyAsset() {
      double assetCurrentValue = await assetClient.GetCurrentValue();

      if (ShouldBuy(assetCurrentValue)) {
        if (lastEmailType != 0) {
          SendEmailToBuy();
          lastEmailType = 0;
        }
      }
      else if (ShouldSell(assetCurrentValue)) {
        if (lastEmailType != 1) {
          SendEmailToSell();
          lastEmailType = 1;
        }
      }
      else {
        lastEmailType = -1;
      }
    }

    private bool ShouldBuy(double assetCurrentValue) {
      return assetCurrentValue < valueToBuy;
    }

    private bool ShouldSell(double assetCurrentValue) {
      return assetCurrentValue > valueToSell;
    }

    void SendEmailToBuy() {
      string title = "Compre ações do ativo " + assetName;
      string text = "O valor da ação " + assetName + " ficou abaixo de " + valueToBuy +
                    ". Aproveite essa oportunidade para comprar!";

      emailClient.SendEmail(title, text, toEmail);
    }

    void SendEmailToSell() {
      string title = "Venda ações do ativo " + assetName;
      string text = "O valor da ação " + assetName + " ficou acima de " + valueToSell +
                    ". Aproveite essa oportunidade para vender!";

      emailClient.SendEmail(title, text, toEmail);
    }
  }
}