using Newtonsoft.Json;
using VRCHypeRate.HeartRateProvider.HypeRate;
using VRCHypeRate.Models;
using VRCHypeRate.Utils;

namespace VRCHypeRate;

public static class Program
{
    private static ConfigModel Config = null!;

    public static void Main()
    {
        Storage.DeleteFile(Logger.LogFilePath);
        setupConfig();
        setupClient();
    }

    private static void setupClient()
    {
        new OscClient.OscClient(new HypeRateProvider(Config.Id, Config.ApiKey)).Start();
        Logger.Log("Press any key to close the program");
        Console.ReadKey(true);
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