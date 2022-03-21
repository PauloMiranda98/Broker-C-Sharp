using System;
using System.IO;
using BrokerCS.services;
using SharpYaml.Serialization;

namespace BrokerCS {
  internal class Program {
    private const int StockNameParams = 0;
    private const int ValueToSellParams = 1;
    private const int ValueToBuyParams = 2;
      
    public static void Main(string[] args) {
      ValidParams(args);

      var config = GetConfig();

      var stockName = args[StockNameParams];
      var valueToSell = double.Parse(args[ValueToSellParams]);
      var valueToBuy = double.Parse(args[ValueToBuyParams]);
      var toEmail = config.ToEmail;
      var stockClient = ConfigStockClient(stockName, config);
      var emailClient = ConfigEmailClient(config);
      
      var broker = new Broker(stockName, valueToSell, valueToBuy, toEmail, stockClient, emailClient);
      broker.Start();
    }
    private static void ValidParams(string[] args) {
      if(args.Length != 3){
        Console.WriteLine("Quantidade de parametros não é suficiente!");
        Console.WriteLine("Execute seguindo este padrão: run.out o_ativo_a_ser_monitorado preco_de_venda preco_de_compra");
        Environment.Exit(0);
      }
    }

    private static Config GetConfig() {
      var input = new StreamReader("../../config.yml");
      var deserializer = new Serializer();
      var config = (Config) deserializer.Deserialize(input, typeof(Config));

      return config;
    }
    private static IStockClient ConfigStockClient(string stockName, Config config) {
      var stockClient = new StockClientByHGBrasil(stockName, config.HGbrasilKey);
      //var stockClient = new StockClientTest(stockName);
      return stockClient;
    }
    private static IEmailClient ConfigEmailClient(Config config) {
      var emailClient = new EmailClient(config.SmtpDomain, config.SmtpPort, config.SmtpUsername, config.SmtpPassword, config.FromEmail);
      //var emailClient = new EmailClientTest(config.FromEmail);
      return emailClient;
    }
  }
}
