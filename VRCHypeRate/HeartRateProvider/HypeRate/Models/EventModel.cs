using Newtonsoft.Json;

namespace VRCHypeRate.HeartRateProvider.HypeRate.Models;

public class EventModel
{
    [JsonProperty("event")]
    public string Event = null!;
}