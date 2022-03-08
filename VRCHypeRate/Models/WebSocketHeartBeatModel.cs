using Newtonsoft.Json;

namespace VRCHypeRate.Models;

public class WebSocketHeartBeatModel
{
    [JsonProperty("topic")]
    private string Topic = "phoenix";

    [JsonProperty("event")]
    private string Event = "heartbeat";

    [JsonProperty("payload")]
    private WebSocketHeartBeatPayload Payload = new();

    [JsonProperty("ref")]
    private int Ref;
}

public class WebSocketHeartBeatPayload { }