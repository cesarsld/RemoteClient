using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using WebSocketSharp;

public class WebSocketClient
{
    static GameManager gameManager;

    static WebSocketSharp.WebSocket ws = new WebSocketSharp.WebSocket("ws://86.160.113.30:1337");

    static WebSocketClient()
    {
        gameManager = UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
        ws.OnMessage += LogMessage;
        ws.OnError += LogMessage;
    }

    public static void Connect()
    {
        ws.Connect();
    }

    public static void SendMessage(string message)
    {
        ws.Send(message);
    }

    public static void Disconnect()
    {
        ws.Close();
    }

    public static void LogMessage(object sender, MessageEventArgs e)
    {
        //UnityEngine.Debug.Log("[Data from server] " + e.Data);
        gameManager.DataFromServerReceived(e.Data);
    }
    public static void LogMessage(object sender, ErrorEventArgs e)
    {
        UnityEngine.Debug.Log("[Data from server] " + e.Message);
    }


}