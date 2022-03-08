using Newtonsoft.Json;
using VRCHypeRate.Models;
using WebSocket4Net;
using ErrorEventArgs = SuperSocket.ClientEngine.ErrorEventArgs;

namespace VRCHypeRate.Client;

public class HypeRateClient
{
    private const string URI = "wss://app.hyperate.io/socket/websocket?token=";
    private readonly string Id;
    private readonly string ApiKey;
    private WebSocket webSocket = null!;
    
    public HypeRateClient(string Id, string ApiKey)
    {
        this.Id = Id;
        this.ApiKey = ApiKey;
    }

    public void Connect()
    {
        var thread = new Thread(connect);
        thread.Start();
    }

    private void connect()
    {
        Console.WriteLine("Attempting connection");
        var URL = URI + ApiKey;
        webSocket = new WebSocket(URL);
        webSocket.Opened += WsConnected;
        webSocket.Error += WsError;
        webSocket.MessageReceived += WsMessageReceived;
        webSocket.OpenAsync();
        while (true) { }
    }

    private void WsError(object? sender, ErrorEventArgs e)
    {
        Console.WriteLine(e.Exception);
    }

    private void WsConnected(object? sender, EventArgs e)
    {
        Console.WriteLine("Successfully connected!");
        sendJoinChannel();
    }

    private void sendJoinChannel()
    {
        Console.WriteLine("Sending join channel");
        var joinChannelModel = new JoinChannelModel
        {
            Id = Id
        };
        webSocket.Send(JsonConvert.SerializeObject(joinChannelModel));
    }

    private void WsMessageReceived(object? sender, MessageReceivedEventArgs e)
    {
        
    }
}