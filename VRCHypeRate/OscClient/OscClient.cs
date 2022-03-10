using System.Net;
using System.Net.Sockets;
using CoreOSC;
using CoreOSC.IO;
using VRCHypeRate.HeartRateProvider;
using VRCHypeRate.Utils;

namespace VRCHypeRate.OscClient;

public class OscClient : UdpClient
{
    private const int OSCPort = 9000;
    private static readonly string OSCURI = IPAddress.Loopback.ToString();

    private readonly BaseHeartRateProvider HeartrateProvider;

    public OscClient(BaseHeartRateProvider heartrateProvider) : base(OSCURI, OSCPort)
    {
        HeartrateProvider = heartrateProvider;
        HeartrateProvider.OnHeartRateUpdate += HandleHeartRateUpdate;
        HeartrateProvider.OnConnected += () => SendParameter(OscParameter.HeartrateEnabled, true);
        HeartrateProvider.OnDisconnected += () => SendParameter(OscParameter.HeartrateEnabled, false);
    }

    public void Start()
    {
        Logger.Log($"Creating OSC client\nURI: {OSCURI}\nPort: {OSCPort}", LogLevel.Debug);
        SendParameter(OscParameter.HeartrateEnabled, false);
        HeartrateProvider.Connect();
    }

    private void HandleHeartRateUpdate(int heartRate)
    {
        SendParameter(OscParameter.HeartrateEnabled, true);

        var normalisedHeartRate = heartRate / 60.0f;
        SendParameter(OscParameter.HeartrateNormalised, normalisedHeartRate);

        var individualValues = heartRate.ToDigitArray(3);
        SendParameter(OscParameter.HeartrateOnes, individualValues[2]);
        SendParameter(OscParameter.HeartrateTens, individualValues[1]);
        SendParameter(OscParameter.HeartrateHundreds, individualValues[0]);
    }

    private void SendParameter(OscParameter parameter, bool value)
    {
        SendParameter(parameter, value ? OscTrue.True : OscFalse.False);
    }

    private void SendParameter(OscParameter parameter, object value)
    {
        Logger.Log($"Sending parameter {parameter} of value {value}", LogLevel.Debug);
        this.SendMessageAsync(new OscMessage(parameter.GetOscAddress(), new[] { value }));
    }
}