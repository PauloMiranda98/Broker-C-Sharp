using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BrokerCS.model.hgbrasil;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace BrokerCS.services {
  public class StockClientByHGBrasil : IStockClient {
    private string stockName;
    private string hgbrasilKey;

    public StockClientByHGBrasil(string stockName, string hgbrasilKey) {
      this.stockName = stockName;
      this.hgbrasilKey = hgbrasilKey;
    }

    public async Task<double> GetCurrentValue() {
      var client = new HttpClient();
      var query = new Dictionary<string, string>(){
        ["key"] = hgbrasilKey,
        ["symbol"] = stockName
      };

      var uri = QueryHelpers.AddQueryString("https://api.hgbrasil.com/finance/stock_price", query);
      var httpResponse = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
      
      httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
      String jsonResponse = await httpResponse.Content.ReadAsStringAsync();
      Stock stock = JsonConvert.DeserializeObject<Stock>(jsonResponse);
      double price = stock.Results[stockName].Price;
      
      Console.WriteLine("O preço da ação " + stockName + " está em " + price + " usando a api do HG Brasil.");
      
      return price;
    }
  }
}