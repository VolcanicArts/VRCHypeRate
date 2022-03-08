using Newtonsoft.Json;

namespace VRCHypeRate.Models;

public class JoinChannelModel
{
    [JsonProperty("topic")]
    private string topic = null!;

    [JsonIgnore]
    public string Id
    {
        set => topic = "hr:" + value;
    }

    [JsonProperty("event")]
    private string Event = "phx_join";

    [JsonProperty("payload")]
    private JoinChannelPayload Payload = new();

    [JsonProperty("ref")]
    private int Ref;
}

public class JoinChannelPayload { }