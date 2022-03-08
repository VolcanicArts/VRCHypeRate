using Newtonsoft.Json;

namespace VRCHypeRate.Models;

public class PhxReplyModel
{
    [JsonProperty("payload")]
    public PhxReplyPayload Payload = null!;
}

public class PhxReplyPayload
{
    [JsonProperty("status")]
    public string Status = null!;
}