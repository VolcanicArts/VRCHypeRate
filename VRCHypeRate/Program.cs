using Newtonsoft.Json;
using VRCHypeRate.Client;
using VRCHypeRate.Models;
using VRCHypeRate.Utils;

namespace VRCHypeRate;

public static class Program
{
    internal static ConfigModel Config = null!;

    public static void Main()
    {
        Storage.DeleteFile(Logger.LogFilePath);
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
            Logger.Error($"{e.Message}\nPlease create a 'config.json' file");
        }
        catch (JsonException e)
        {
            Logger.Error($"{e.Message}\nPlease make sure your `config.json` file is formatted correctly");
        }
    }
}