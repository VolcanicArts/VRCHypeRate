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
        var config = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText("./config.json"));
        if (config == null)
        {
            Logger.Log("Please define a config (config.json)");
            return;
        }

        Config = config;

        var client = new HypeRateClient(Config.Id, Config.ApiKey);
        client.Connect();
    }
}