﻿using System.Net.Sockets;
using CoreOSC;
using CoreOSC.IO;
using VRCHypeRate.Client;

namespace VRCHypeRate.Utils;

public static class OscExtensions
{
    public static void SendParameter(this UdpClient oscClient, OSCParameter parameter, object value)
    {
        Logger.Log($"Sending parameter {parameter.ToString()} of value {value}", LogLevel.Debug);
        var message = new OscMessage(new Address($"/avatar/parameters/{parameter.ToString()}"), new[] { value });
        oscClient.SendMessageAsync(message);
    }
}