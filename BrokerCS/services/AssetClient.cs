using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BrokerCS.model.hgbrasil;
using BrokerCS.services;
using Microsoft.AspNetCore.WebUtilities;
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
      var query = new Dictionary<string, string>(){
        ["key"] = hgbrasilKey,
        ["symbol"] = assetName
      };

      var uri = QueryHelpers.AddQueryString("https://api.hgbrasil.com/finance/stock_price", query);
      var httpResponse = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
      
      httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
      String jsonResponse = await httpResponse.Content.ReadAsStringAsync();
      Asset asset = JsonConvert.DeserializeObject<Asset>(jsonResponse);
      double price = asset.Results[assetName].Price;
      
      Console.WriteLine("O preço da ação " + assetName + " está em " + price + " usando a api do HG Brasil.");
      
      return price;
    }
  }
}