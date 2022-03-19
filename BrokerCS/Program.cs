using System;
using System.IO;
using SharpYaml.Serialization;

namespace BrokerCS {
  internal class Program {
    public static void Main(string[] args) {
      ValidParams(args);

      var config = GetConfig();

      var asset = ConfigAssetWatcher(args);
      var emailClient = ConfigEmailClient(config);
      var toEmail = config.ToEmail;
      
      var broker = new Broker(asset, emailClient, toEmail);
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
    private static AssetWatcher ConfigAssetWatcher(string[] args) {
      var assetName = args[0];
      var valueToSell = double.Parse(args[1]);
      var valueToBuy = double.Parse(args[2]);

      var asset = new AssetWatcher(assetName, valueToSell, valueToBuy);
      return asset;
    }
    private static EmailClient ConfigEmailClient(Config config) {
      EmailClient emailClient = new EmailClient(config.SmtpDomain, config.SmtpPort, config.SmtpUsername, config.SmtpPassword, config.FromEmail);
      return emailClient;
    }

    private static string GetEmail() {
      return "paulomiranda12@gmail.com";
    }
  }
}
