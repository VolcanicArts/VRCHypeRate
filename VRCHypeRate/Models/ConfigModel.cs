using Newtonsoft.Json;

namespace VRCHypeRate.Models;

public class ConfigModel
{
    [JsonProperty("apikey")]
    public string ApiKey;

    [JsonProperty("id")]
    public string Id;
}