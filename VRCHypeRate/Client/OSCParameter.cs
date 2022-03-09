using CoreOSC;

namespace VRCHypeRate.Client;

public enum OSCParameter
{
    HeartrateEnabled,
    HeartrateNormalised,
    HeartrateOnes,
    HeartrateTens,
    HeartrateHundreds
}

public static class OSCParameterExtensions
{
    public static Address GetOscAddress(this OSCParameter parameter)
    {
        return new Address($"/avatar/parameters/{parameter.ToString()}");
    }
}