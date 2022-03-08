using System.Net;
using System.Net.Sockets;
using CoreOSC;
using CoreOSC.IO;
using Newtonsoft.Json;
using VRCHypeRate.Models;
using VRCHypeRate.Utils;
using WebSocket4Net;
using ErrorEventArgs = SuperSocket.ClientEngine.ErrorEventArgs;

namespace VRCHypeRate.Client;

public class HypeRateClient
{
    private const string URI = "wss://app.hyperate.io/socket/websocket?token=";
    private static readonly string OSCURI = IPAddress.Loopback.ToString();
    private const int OSCPort = 9000;
    private const int HeartbeatInternal = 30000;
    
    private readonly string Id;
    private readonly string ApiKey;
    private WebSocket webSocket = null!;
    private UdpClient oscClient = null!;
    private Timer heartBeatTimer = null!;
    private bool IsRunning = true;
    private readonly Logger Logger = Logger.GetLogger(nameof(HypeRateClient));

    public HypeRateClient(string Id, string ApiKey)
    {
        this.Id = Id;
        this.ApiKey = ApiKey;
    }

    public void Connect()
    {
        new Thread(connect).Start();
    }

    private void connect()
    {
        setupWebSocket();
        setupOscClient();
        while (IsRunning) { }
    }

    private void setupWebSocket()
    {
        Logger.Log($"Creating websocket\nURI: {URI}\nApiKey: {ApiKey}", LogLevel.Debug);
        webSocket = new WebSocket(URI + ApiKey);
        webSocket.Opened += WsConnected;
        webSocket.Closed += WsDisconnected;
        webSocket.Error += WsError;
        webSocket.MessageReceived += WsMessageReceived;
        Logger.Log("Attempting to connect to HypeRate websocket");
        webSocket.OpenAsync();
    }

    private void setupOscClient()
    {
        Logger.Log($"Creating OSC client\nURI: {OSCURI}\nPort: {OSCPort}", LogLevel.Debug);
        oscClient = new UdpClient(OSCURI, OSCPort);
        sendParameter("HeartrateEnabled", OscFalse.False);
    }

    private void WsDisconnected(object? sender, EventArgs e)
    {
        Logger.Log("Websocket has disconnected");
        heartBeatTimer.Dispose();
        IsRunning = false;
        sendParameter("HeartrateEnabled", OscFalse.False);
    }

    private void WsError(object? sender, ErrorEventArgs e)
    {
        Logger.Log(e.Exception.ToString());
    }

    private void WsConnected(object? sender, EventArgs e)
    {
        Logger.Log("Successfully connected!");
        sendJoinChannel();
        initHeartBeat();
        sendParameter("HeartrateEnabled", OscTrue.True);
    }

    private void initHeartBeat()
    {
        heartBeatTimer = new Timer(sendHeartBeat, null, HeartbeatInternal, Timeout.Infinite);
    }

    private void sendHeartBeat(object? _)
    {
        Logger.Log("Sending heartbeat to websocket");
        webSocket.Send(JsonConvert.SerializeObject(new WebSocketHeartBeatModel()));
        heartBeatTimer.Change(HeartbeatInternal, Timeout.Infinite);
    }

    private void sendJoinChannel()
    {
        Logger.Log("Requesting to join channel for websocket");
        var joinChannelModel = new JoinChannelModel
        {
            Id = Id
        };
        webSocket.Send(JsonConvert.SerializeObject(joinChannelModel));
    }

    private void WsMessageReceived(object? sender, MessageReceivedEventArgs e)
    {
        Logger.Log(e.Message, LogLevel.Debug);
        var eventModel = JsonConvert.DeserializeObject<EventModel>(e.Message);
        if (eventModel == null)
        {
            Logger.Log($"Received an unrecognised message:\n{e.Message}");
            return;
        }

        switch (eventModel.Event)
        {
            case "hr_update":
                handleHrUpdate(JsonConvert.DeserializeObject<HeartRateUpdateModel>(e.Message)!);
                break;
            case "phx_reply":
                handlePhxReply(JsonConvert.DeserializeObject<PhxReplyModel>(e.Message)!);
                break;
        }
    }

    private void handlePhxReply(PhxReplyModel reply)
    {
        Logger.Log($"Status of reply: {reply.Payload.Status}");
    }

    private void handleHrUpdate(HeartRateUpdateModel update)
    {
        var heartRate = update.Payload.HeartRate;
        Logger.Log($"Received heartrate {heartRate}");

        switch (Program.Config.Mode)
        {
            case Modes.Normalised:
                var normalisedHeartRate = (heartRate / 60.0f);
                sendParameter("Heartrate", normalisedHeartRate);
                break;
            case Modes.Individual:
                var individualValues = getIntArray(heartRate);
                sendParameter("HeartrateOnes", individualValues[2]);
                sendParameter("HeartrateTens", individualValues[1]);
                sendParameter("HeartrateHundreds", individualValues[0]);
                break;
            default:
                Logger.Log("Invalid mode defined");
                break;
        }
    }

    private void sendParameter(string name, object value)
    {
        Logger.Log($"Sending parameter {name} of value {value}", LogLevel.Debug);
        var message = new OscMessage(new Address($"/avatar/parameters/{name}"), new[] { value });
        oscClient.SendMessageAsync(message);
    }

    private static int[] getIntArray(int num)
    {
        var numStr = num.ToString().PadLeft(3, '0');
        var intList = numStr.Select(digit => int.Parse(digit.ToString()));
        return intList.ToArray();
    }
}