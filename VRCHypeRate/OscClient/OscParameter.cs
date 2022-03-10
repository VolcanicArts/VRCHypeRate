using CoreOSC;

namespace VRCHypeRate.OscClient;

public enum OscParameter
{
    HeartrateEnabled,
    HeartrateNormalised,
    HeartrateOnes,
    HeartrateTens,
    HeartrateHundreds
}

public static class OscParameterExtensions
{
    public static Address GetOscAddress(this OscParameter parameter)
    {
        return new Address($"/avatar/parameters/{parameter.ToString()}");
    }
}