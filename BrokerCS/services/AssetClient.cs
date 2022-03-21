using System;
using System.Net.Http;
using System.Threading.Tasks;
using BrokerCS.model.hgbrasil;
using BrokerCS.services;
using Newtonsoft.Json;

namespace BrokerCS {
  public class AssetClientByHGBrasil : IAssetClient {
    private string assetName { get; set; }

    private string hgbrasilKey;

    public AssetClientByHGBrasil(string assetName, string hgbrasilKey) {
      this.assetName = assetName;
      this.hgbrasilKey = hgbrasilKey;
    }

    public async Task<double> GetCurrentValue() {
      var client = new HttpClient();
      var httpResponse = await client.GetAsync("https://api.hgbrasil.com/finance/stock_price?key=" + hgbrasilKey + "&symbol=" + assetName, HttpCompletionOption.ResponseHeadersRead);
      
      httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
      String myJsonResponse = await httpResponse.Content.ReadAsStringAsync();
      Asset root = JsonConvert.DeserializeObject<Asset>(myJsonResponse);
      double price = root.Results[assetName].Price;
      
      Console.WriteLine("O preço da ação " + assetName + " está em " + price + " usando a api do HG Brasil.");
      
      return price;
    }
  }
}