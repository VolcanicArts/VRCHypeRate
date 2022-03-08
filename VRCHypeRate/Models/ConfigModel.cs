using Newtonsoft.Json;

namespace VRCHypeRate.Models;

public class ConfigModel
{
    [JsonProperty("id")]
    public string Id;

    [JsonProperty("apikey")]
    public string ApiKey;

    [JsonProperty("mode")]
    public Modes Mode;
}

public enum Modes
{
    Normalised,
    Individual
}