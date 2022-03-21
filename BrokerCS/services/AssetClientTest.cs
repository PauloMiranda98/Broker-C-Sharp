using System;
using System.IO;
using System.Threading.Tasks;

namespace BrokerCS.services {
  public class AssetClientTest : IAssetClient{
    private string assetName { get; set; }

    public AssetClientTest(string assetName) {
      this.assetName = assetName;
    }

    public async Task<double> GetCurrentValue() {
      var lines = File.ReadAllLines("../../fake_asset_value.txt");
      var price = double.Parse(lines[0]);

      Console.WriteLine("O preço da ação " + assetName + " está em " + price + " usando um arquivo de teste.");

      return price;
    }
  }
}