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
    private readonly EventWaitHandle IsRunning = new AutoResetEvent(false);

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
        Task.Factory.StartNew(run).Wait();
    }

    protected void Send(ISendableModel data)
    {
        WebSocket.Send(JsonConvert.SerializeObject(data));
    }

    private void run()
    {
        WebSocket.Open();
        IsRunning.WaitOne();
    }

    private void WsConnected(object? sender, EventArgs e)
    {
        Logger.Log("WebSocket successfully connected", LogLevel.Debug);
        OnConnected?.Invoke();
        OnWsConnected();
    }

    protected abstract void OnWsConnected();

    private void WsDisconnected(object? sender, EventArgs e)
    {
        Logger.Log("WebSocket disconnected", LogLevel.Debug);
        OnDisconnected?.Invoke();
        OnWsDisconnected();
        IsRunning.Set();
    }

    protected abstract void OnWsDisconnected();

    private void WsMessageReceived(object? sender, MessageReceivedEventArgs e)
    {
        Logger.Log(e.Message, LogLevel.Debug);
        OnWsMessageReceived(e.Message);
    }

    protected abstract void OnWsMessageReceived(string message);

    private static void WsError(object? sender, ErrorEventArgs e)
    {
        Logger.Log(e.Exception.ToString(), LogLevel.Error);
    }
}