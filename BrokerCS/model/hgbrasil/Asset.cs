using System.Collections.Generic;
using Newtonsoft.Json;

namespace BrokerCS.model.hgbrasil {
  public class MarketTime {
    [JsonProperty("open")]
    public string Open { get; set; }
  
    [JsonProperty("close")]
    public string Close { get; set; }
  
    [JsonProperty("timezone")]
    public int Timezone { get; set; }
  }
  
  public class Result {
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
  
    [JsonProperty("name")]
    public string Name { get; set; }
  
    [JsonProperty("company_name")]
    public string CompanyName { get; set; }
  
    [JsonProperty("document")]
    public string Document { get; set; }
  
    [JsonProperty("description")]
    public string Description { get; set; }
  
    [JsonProperty("website")]
    public string Website { get; set; }
  
    [JsonProperty("region")]
    public string Region { get; set; }
  
    [JsonProperty("currency")]
    public string Currency { get; set; }
  
    [JsonProperty("market_time")]
    public MarketTime MarketTime { get; set; }
  
    [JsonProperty("market_cap")]
    public double MarketCap { get; set; }
  
    [JsonProperty("price")]
    public double Price { get; set; }
  
    [JsonProperty("change_percent")]
    public double ChangePercent { get; set; }
  
    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; }
  }
  
  public class Asset {
    [JsonProperty("by")]
    public string By { get; set; }
  
    [JsonProperty("valid_key")]
    public bool ValidKey { get; set; }
  
    [JsonProperty("results")]
    public Dictionary<string, Result> Results { get; set; }
  
    [JsonProperty("execution_time")]
    public double ExecutionTime { get; set; }
  
    [JsonProperty("from_cache")]
    public bool FromCache { get; set; }
  }
}