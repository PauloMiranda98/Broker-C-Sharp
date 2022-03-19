using System;
using System.IO;

namespace BrokerCS {
  public class AssetWatcher {
    public string assetName { get; set; }
    public double valueToSell { get; set; }
    public double valueToBuy { get; set; }

    public AssetWatcher(string assetName, double valueToSell, double valueToBuy) {
      this.assetName = assetName;
      this.valueToSell = valueToSell;
      this.valueToBuy = valueToBuy;
    }

    public double GetCurrentValue() {
      var lines = File.ReadAllLines("../../fake_asset_value.txt");

      return double.Parse(lines[0]);
    }

    public bool ShouldBuy() {
      return GetCurrentValue() < valueToBuy;
    }

    public bool ShouldSell() {
      return GetCurrentValue() > valueToSell;
    }
  }
}