using System.Threading;
using BrokerCS.services;

namespace BrokerCS {
  public class Broker {
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
      this.stockClient = stockClient;
      this.emailClient = emailClient;
      this.toEmail = toEmail;
    }

    public void Start() {
      while (true) {
        VerifyStockQuote();
        Thread.Sleep(10000);
      }
    }
    async void VerifyStockQuote() {
      double stockCurrentValue = await stockClient.GetCurrentValue();

      if (ShouldBuy(stockCurrentValue)) {
        if (lastEmailType != 0) {
          SendEmailToBuy();
          lastEmailType = 0;
        }
      }
      else if (ShouldSell(stockCurrentValue)) {
        if (lastEmailType != 1) {
          SendEmailToSell();
          lastEmailType = 1;
        }
      }
      else {
        lastEmailType = -1;
      }
    }

    private bool ShouldBuy(double stockCurrentValue) {
      return stockCurrentValue < valueToBuy;
    }

    private bool ShouldSell(double stockCurrentValue) {
      return stockCurrentValue > valueToSell;
    }

    void SendEmailToBuy() {
      string title = "Compre ações do ativo " + stockName;
      string text = "O valor da ação " + stockName + " ficou abaixo de " + valueToBuy +
                    ". Aproveite essa oportunidade para comprar!";

      emailClient.SendEmail(title, text, toEmail);
    }

    void SendEmailToSell() {
      string title = "Venda ações do ativo " + stockName;
      string text = "O valor da ação " + stockName + " ficou acima de " + valueToSell +
                    ". Aproveite essa oportunidade para vender!";

      emailClient.SendEmail(title, text, toEmail);
    }
  }
}