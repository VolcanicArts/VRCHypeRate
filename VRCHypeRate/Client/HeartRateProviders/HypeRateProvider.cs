using Newtonsoft.Json;
using VRCHypeRate.Models;
using VRCHypeRate.Utils;
using WebSocket4Net;

namespace VRCHypeRate.Client;

public class HypeRateProvider : BaseHeartRateProvider
{
    private const string HypeRateUri = "wss://app.hyperate.io/socket/websocket?token=";
    private const int HeartbeatInternal = 30000;
    private readonly string Id;
    private Timer? heartBeatTimer;

    public HypeRateProvider(string Id, string ApiKey) : base(HypeRateUri + ApiKey)
    {
        this.Id = Id;
    }

    protected override void OnWsConnected()
    {
        Logger.Log("Successfully connected to the HypeRate websocket");
        sendJoinChannel();
        initHeartBeat();
    }

    protected override void OnWsDisconnected()
    {
        Logger.Log("Disconnected from the HypeRate websocket");
        heartBeatTimer?.Dispose();
    }

    protected override void OnWsMessageReceived(string message)
    {
        var eventModel = JsonConvert.DeserializeObject<EventModel>(message);
        if (eventModel == null)
        {
            Logger.Log($"Received an unrecognised message:\n{message}");
            return;
        }

        switch (eventModel.Event)
        {
            case "hr_update":
                handleHrUpdate(JsonConvert.DeserializeObject<HeartRateUpdateModel>(message)!);
                break;
            case "phx_reply":
                handlePhxReply(JsonConvert.DeserializeObject<PhxReplyModel>(message)!);
                break;
        }
    }

    private void initHeartBeat()
    {
        heartBeatTimer = new Timer(sendHeartBeat, null, HeartbeatInternal, Timeout.Infinite);
    }

    private void sendHeartBeat(object? _)
    {
        Logger.Log("Sending HypeRate websocket heartbeat");
        Send(new WebSocketHeartBeatSendableModel());
        heartBeatTimer?.Change(HeartbeatInternal, Timeout.Infinite);
    }

    private void sendJoinChannel()
    {
        Logger.Log($"Requesting to hook into heartrate for Id {Id}");
        var joinChannelModel = new JoinChannelSendableModel
        {
            Id = Id
        };
        Send(joinChannelModel);
    }

    private static void handlePhxReply(PhxReplyModel reply)
    {
        Logger.Log($"Status of reply: {reply.Payload.Status}");
    }

    private void handleHrUpdate(HeartRateUpdateModel update)
    {
        var heartRate = update.Payload.HeartRate;
        Logger.Log($"Received heartrate {heartRate}");
        OnHeartRateUpdate?.Invoke(heartRate);
    }
}