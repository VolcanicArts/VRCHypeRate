using System.Net;
using System.Net.Sockets;
using CoreOSC;
using CoreOSC.IO;
using VRCHypeRate.Utils;

namespace VRCHypeRate.Client;

public class OscClient : UdpClient
{
    private const int OSCPort = 9000;
    private static readonly string OSCURI = IPAddress.Loopback.ToString();

    private readonly BaseHeartRateProvider HeartrateProvider;

    public OscClient(BaseHeartRateProvider heartrateProvider) : base(OSCURI, OSCPort)
    {
        HeartrateProvider = heartrateProvider;
        HeartrateProvider.OnHeartRateUpdate += HandleHeartRateUpdate;
        HeartrateProvider.OnConnected += () => SendParameter(OSCParameter.HeartrateEnabled, true);
        HeartrateProvider.OnDisconnected += () => SendParameter(OSCParameter.HeartrateEnabled, false);
    }

    public void Start()
    {
        Logger.Log($"Creating OSC client\nURI: {OSCURI}\nPort: {OSCPort}", LogLevel.Debug);
        SendParameter(OSCParameter.HeartrateEnabled, false);
        HeartrateProvider.Connect();
    }

    private void HandleHeartRateUpdate(int heartRate)
    {
        SendParameter(OSCParameter.HeartrateEnabled, true);

        var normalisedHeartRate = heartRate / 60.0f;
        SendParameter(OSCParameter.HeartrateNormalised, normalisedHeartRate);

        var individualValues = heartRate.ToDigitArray(3);
        SendParameter(OSCParameter.HeartrateOnes, individualValues[2]);
        SendParameter(OSCParameter.HeartrateTens, individualValues[1]);
        SendParameter(OSCParameter.HeartrateHundreds, individualValues[0]);
    }

    private void SendParameter(OSCParameter parameter, bool value)
    {
        SendParameter(parameter, value ? OscTrue.True : OscFalse.False);
    }

    private void SendParameter(OSCParameter parameter, object value)
    {
        Logger.Log($"Sending parameter {parameter.GetOscAddress()} of value {value}", LogLevel.Debug);
        this.SendMessageAsync(new OscMessage(parameter.GetOscAddress(), new[] { value }));
    }
}