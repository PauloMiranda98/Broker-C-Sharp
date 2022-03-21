using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BrokerCS.services {
  public interface IAssetClient {
    Task<double> GetCurrentValue();
  }
}