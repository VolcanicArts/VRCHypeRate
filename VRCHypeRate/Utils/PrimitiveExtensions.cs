using CoreOSC;

namespace VRCHypeRate.Utils;

public static class PrimitiveExtensions
{
    public static int[] ToDigitArray(this int num, int totalSize)
    {
        var numStr = num.ToString().PadLeft(totalSize, '0');
        var intList = numStr.Select(digit => int.Parse(digit.ToString()));
        return intList.ToArray();
    }

    public static object ToOscBoolean(this bool value)
    {
        return value ? OscTrue.True : OscFalse.False;
    }
}