using Newtonsoft.Json;
using VRCHypeRate.Client;
using VRCHypeRate.Models;
using VRCHypeRate.Utils;

namespace VRCHypeRate;

public static class Program
{
    private static readonly Logger Logger = Logger.GetLogger("VRCHypeRate");
    internal static ConfigModel Config = null!;

    public static void Main()
    {
        setupConfig();
        setupClient();
    }

    private static void setupClient()
    {
        new HypeRateClient(Config.Id, Config.ApiKey).Connect();
    }

    private static void setupConfig()
    {
        try
        {
            Config = Storage.GetFileAsJson<ConfigModel>("./config.json");
        }
        catch (FileNotFoundException e)
        {
            Logger.Log($"{e.Message}\nPlease create a 'config.json' file");
            Console.ReadLine();
        }
        catch (JsonException e)
        {
            Logger.Log($"{e.Message}\nPlease make sure your `config.json` file is formatted correctly");
            Console.ReadLine();
        }
    }
}