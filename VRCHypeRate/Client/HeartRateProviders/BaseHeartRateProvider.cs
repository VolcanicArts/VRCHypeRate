using Newtonsoft.Json;
using VRCHypeRate.Models;
using VRCHypeRate.Utils;
using WebSocket4Net;
using ErrorEventArgs = SuperSocket.ClientEngine.ErrorEventArgs;

namespace VRCHypeRate.Client;

public abstract class BaseHeartRateProvider
{
    public Action? OnConnected;
    public Action? OnDisconnected;
    public Action<int>? OnHeartRateUpdate;

    private readonly WebSocket WebSocket;
    private bool IsRunning = true;

    protected BaseHeartRateProvider(string Uri)
    {
        Logger.Log("Creating base websocket", LogLevel.Debug);
        WebSocket = new WebSocket(Uri);
        WebSocket.Opened += WsConnected;
        WebSocket.Closed += WsDisconnected;
        WebSocket.MessageReceived += WsMessageReceived;
        WebSocket.Error += WsError;
    }

    public void Connect()
    {
        new Thread(run).Start();
    }

    protected void Send(ISendableModel data)
    {
        WebSocket.Send(JsonConvert.SerializeObject(data));
    }

    private void run()
    {
        WebSocket.OpenAsync();
        while (IsRunning) { }
    }

    protected virtual void WsConnected(object? sender, EventArgs e)
    {
        Logger.Log("WebSocket successfully connected", LogLevel.Debug);
        OnConnected?.Invoke();
    }

    protected virtual void WsDisconnected(object? sender, EventArgs e)
    {
        Logger.Log("WebSocket disconnected", LogLevel.Debug);
        OnDisconnected?.Invoke();
        IsRunning = false;
    }

    protected virtual void WsMessageReceived(object? sender, MessageReceivedEventArgs e)
    {
        Logger.Log(e.Message, LogLevel.Debug);
    }

    private static void WsError(object? sender, ErrorEventArgs e)
    {
        Logger.Log(e.Exception.ToString(), LogLevel.Error);
    }
}