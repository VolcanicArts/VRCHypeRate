using Newtonsoft.Json;

namespace VRCHypeRate.Models;

public class WebSocketHeartBeatSendableModel : ISendableModel
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