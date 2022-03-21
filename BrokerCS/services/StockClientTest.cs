using System;
using System.IO;
using System.Threading.Tasks;

namespace BrokerCS.services {
  public class StockClientTest : IStockClient {
    private string stockName;

    public StockClientTest(string stockName) {
      this.stockName = stockName;
    }

    public async Task<double> GetCurrentValue() {
      var lines = File.ReadAllLines("../../fake_stock_value.txt");
      var price = double.Parse(lines[0]);

      Console.WriteLine("O preço da ação " + stockName + " está em " + price + " usando um arquivo de teste.");

      return price;
    }
  }
}