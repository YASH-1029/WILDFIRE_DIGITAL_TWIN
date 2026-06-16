using UnityEngine;
using TMPro;
using System;
using System.IO;

public class EventLogger : MonoBehaviour
{
    public TMP_Text logText;

    private string logs = "";

    EventList eventList = new EventList();

    void Start()
    {
        logText.text = "EVENT LOG\n\nWaiting...";
    }

    public void AddLog(string message)
    {
        string timeStamp = DateTime.Now.ToString("HH:mm:ss");

        logs = "[" + timeStamp + "] " + message + "\n\n" + logs;

        logText.text = "EVENT LOG\n\n" + logs;

        EventData newEvent = new EventData();

        newEvent.time = timeStamp;
        newEvent.eventMessage = message;

        eventList.events.Add(newEvent);

        SaveToJson();
    }

    void SaveToJson()
    {
        string json = JsonUtility.ToJson(eventList, true);

        string path = Application.dataPath + "/eventLogs.json";

        File.WriteAllText(path, json);
    }
}