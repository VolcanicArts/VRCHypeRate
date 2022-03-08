using Newtonsoft.Json;
using VRCHypeRate.Client;
using VRCHypeRate.Models;

namespace VRCHypeRate;

public static class Program
{
    public static void Main()
    {
        var config = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText("./config.json"))!;
        var client = new HypeRateClient(config.Id, config.ApiKey);
        client.Connect();
    }
}