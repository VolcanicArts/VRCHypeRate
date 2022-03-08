using Newtonsoft.Json;

namespace VRCHypeRate.Models;

public class HeartRateUpdateModel
{
    [JsonProperty("payload")]
    public HeartRateUpdatePayload Payload = null!;
}

public class HeartRateUpdatePayload
{
    [JsonProperty("hr")]
    public int HeartRate;
}