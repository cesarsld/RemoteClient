using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CollectAndSendData : MonoBehaviour {

    public InputField inputField;
    public WebSocket ws = new WebSocket(new Uri("ws://86.160.113.30:1337"));
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendData()
    {
        string message = inputField.text;
        Debug.Log("send message :" + message);
        WebSocketClient.SendMessage(message);
    }
    public void Connect()
    {
        WebSocketClient.Connect();
        //StartCoroutine(ws.Connect());
    }

    public void Disconnect()
    {
        //ws.Close();
        WebSocketClient.Disconnect();
    }

}
