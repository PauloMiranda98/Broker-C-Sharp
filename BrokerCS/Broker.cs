using System.Threading;
using BrokerCS.services;

namespace BrokerCS {
  public class Broker {
    private const int DefaultEmailType = -1;
    private const int BuyEmailType = 0;
    private const int SellEmailType = 1;
    private const int DelayTimeInMs = 10000;
    
    private string toEmail;
    private string stockName;
    private double valueToSell;
    private double valueToBuy;
    private IStockClient stockClient;
    private IEmailClient emailClient;
    private int lastEmailType = -1;

    public Broker(string stockName, double valueToSell, double valueToBuy, string toEmail, IStockClient stockClient, IEmailClient emailClient) {
      this.stockName = stockName;
      this.valueToSell = valueToSell;
      this.valueToBuy = valueToBuy;
      this.toEmail = toEmail;

      this.stockClient = stockClient;
      this.emailClient = emailClient;
    }

    public void Start() {
      while (true) {
        VerifyStockQuote();
        Thread.Sleep(DelayTimeInMs);
      }
    }
    private async void VerifyStockQuote() {
      var stockCurrentValue = await stockClient.GetCurrentValue();

      if (ShouldBuy(stockCurrentValue)) {
        if (lastEmailType != BuyEmailType) {
          SendEmailToBuy();
          lastEmailType = BuyEmailType;
        }
      }
      else if (ShouldSell(stockCurrentValue)) {
        if (lastEmailType != SellEmailType) {
          SendEmailToSell();
          lastEmailType = SellEmailType;
        }
      }
      else {
        lastEmailType = DefaultEmailType;
      }
    }

    private bool ShouldBuy(double stockCurrentValue) {
      return stockCurrentValue < valueToBuy;
    }

    private bool ShouldSell(double stockCurrentValue) {
      return stockCurrentValue > valueToSell;
    }

    private void SendEmailToBuy() {
      var title = "Compre ações do ativo " + stockName;
      var text = "O valor da ação " + stockName + " ficou abaixo de " + valueToBuy +
                 ". Aproveite essa oportunidade para comprar!";

      emailClient.SendEmail(title, text, toEmail);
    }

    private void SendEmailToSell() {
      var title = "Venda ações do ativo " + stockName;
      var text = "O valor da ação " + stockName + " ficou acima de " + valueToSell +
                 ". Aproveite essa oportunidade para vender!";

      emailClient.SendEmail(title, text, toEmail);
    }
  }
}