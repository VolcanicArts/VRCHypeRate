using Newtonsoft.Json;

namespace VRCHypeRate.HeartRateProvider.HypeRate.Models;

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