using Newtonsoft.Json;
using VRCHypeRate.Client;
using VRCHypeRate.Models;
using VRCHypeRate.Utils;

namespace VRCHypeRate;

public static class Program
{
    private static readonly Logger Logger = Logger.GetLogger("VRCHypeRate");
    public static ConfigModel Config = null!;
    public static void Main()
    {
        var configFile = string.Empty;
        try
        {
            configFile = File.ReadAllText("./config.json");
        }
        catch (FileNotFoundException)
        {
            Logger.Log("Please define a config (config.json)");
            Console.ReadLine();
            return;
        }

        var config = JsonConvert.DeserializeObject<ConfigModel>(configFile);
        if (config == null)
        {
            Logger.Log("Please define a valid config (config.json)");
            Console.ReadLine();
            return;
        }

        Config = config;

        var client = new HypeRateClient(Config.Id, Config.ApiKey);
        client.Connect();
    }
}