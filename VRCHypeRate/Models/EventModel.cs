using Newtonsoft.Json;

namespace VRCHypeRate.Models;

public class EventModel
{
    [JsonProperty("event")]
    public string Event = null!;
}