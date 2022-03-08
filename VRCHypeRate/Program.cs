using Newtonsoft.Json;
using VRCHypeRate.Client;
using VRCHypeRate.Models;

namespace VRCHypeRate;

public static class Program
{
    public static ConfigModel Config;
    public static void Main()
    {
        Config = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText("./config.json"))!;
        var client = new HypeRateClient(Config.Id, Config.ApiKey);
        client.Connect();
    }
}