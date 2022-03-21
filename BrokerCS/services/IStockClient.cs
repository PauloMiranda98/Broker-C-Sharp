using System.Threading.Tasks;

namespace BrokerCS.services {
  public interface IStockClient {
    Task<double> GetCurrentValue();
  }
}