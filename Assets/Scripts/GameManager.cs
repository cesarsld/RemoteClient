using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int maxMessage = 50;

    private List<string> serverIncomeDataList = new List<string>();
    private WebSocket ws;
    public GameObject chatPanel;
    public GameObject textObject;
    public InputField inputField;
    public ScrollRect scrollRect;


    [SerializeField]
    private List<Message> messageList = new List<Message>();
	// Use this for initialization
	void Start () {
        ws = GameObject.Find("SendDataObject").GetComponent<CollectAndSendData>().ws;
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (!inputField.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(inputField.text);
                WebSocketClient.SendMessage(inputField.text);
                //ws.Send(inputField.text);
            }
            inputField.ActivateInputField();
        }
        if (serverIncomeDataList.Count > 0)
        {
            SendMessageToChat(serverIncomeDataList[0]);
            serverIncomeDataList.RemoveAt(0);
        }
        //string reply = ws.Recv();
        //if (reply != null)
        //{
        //    SendMessageToChat(reply);
        //}
	}
    public void SendMessageToChat(string text)
    {
        if (messageList.Count > maxMessage)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.RemoveAt(0);
        }
        Message message = new Message();
        message.text = " " + text;
        GameObject newTextObject;
        if (scrollRect.verticalNormalizedPosition < 0.1f)
        {
            newTextObject = Instantiate(textObject, chatPanel.transform);
            message.textObject = newTextObject.GetComponent<Text>();
            Canvas.ForceUpdateCanvases();
            chatPanel.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
            chatPanel.GetComponent<ContentSizeFitter>().SetLayoutVertical();

            scrollRect.content.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
            scrollRect.content.GetComponent<ContentSizeFitter>().SetLayoutVertical();

            scrollRect.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
        }
        else
        {
            newTextObject = Instantiate(textObject, chatPanel.transform);
            message.textObject = newTextObject.GetComponent<Text>();
            Debug.Log(scrollRect.verticalNormalizedPosition);
        }
        //Canvas.ForceUpdateCanvases();

        message.textObject.text = message.text;

        messageList.Add(message);
    }

    public void DataFromServerReceived(string message)
    {
        serverIncomeDataList.Add(message);
    }

}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}