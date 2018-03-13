using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArduinoManager : MonoBehaviour {

    //public ArduinoConnector ardCon;
    public ArduinoThread ardThread;
    
    // Use this for initialization
    void Start () {
        //ardCon = new ArduinoConnector();
        //ardCon.Open();
        ardThread = new ArduinoThread();
        ardThread.StartThread();
    }

    // Update is called once per frame
    void Update()
    {
        // If user presses p, send PING
        if (Input.GetKeyUp("p"))
        {
            ardThread.SendToArduino("PING");
            //Debug.Log("sent to arduino");
        }
        string fromArduino = ardThread.ReadFromArduino();
        //Debug.Log(ardThread.ReadFromArduino());
        Debug.Log("From arduino: " + fromArduino);
        if (fromArduino == "a1b")
        {
            Messenger.Broadcast(GameEvent.A1_BLUE);
        }
        if (fromArduino == "a1r")
        {
            Messenger.Broadcast(GameEvent.A1_RED);
        }
        if (fromArduino == "a2b")
        {
            Messenger.Broadcast(GameEvent.A2_BLUE);
        }
        if (fromArduino == "a2r")
        {
            Messenger.Broadcast(GameEvent.A2_RED);
        }
        if (fromArduino == "a3b")
        {
            Messenger.Broadcast(GameEvent.A3_BLUE);
        }
        if (fromArduino == "a3r")
        {
            Messenger.Broadcast(GameEvent.A3_RED);
        }
        if (fromArduino == "a4b")
        {
            Messenger.Broadcast(GameEvent.A4_BLUE);
        }
        if (fromArduino == "a4r")
        {
            Messenger.Broadcast(GameEvent.A4_RED);
        }
        if (fromArduino == "a5b")
        {
            Messenger.Broadcast(GameEvent.A5_BLUE);
        }
        if (fromArduino == "a5r")
        {
            Messenger.Broadcast(GameEvent.A5_RED);
        }
        if (fromArduino == "p1d")
        {
            Debug.Log("Arduino P1 Triggered");
            Messenger.Broadcast(GameEvent.P1_BTN_DOWN);
        }
        if (fromArduino == "p1u")
        {
            Debug.Log("Arduino P1 Triggered");
            Messenger.Broadcast(GameEvent.P1_BTN_UP);
        }
        if (fromArduino == "p2d")
        {
            Debug.Log("Arduino P2 Triggered");
            Messenger.Broadcast(GameEvent.P2_BTN_DOWN);
        }
        if (fromArduino == "p2u")
        {
            Debug.Log("Arduino P2 Triggered");
            Messenger.Broadcast(GameEvent.P2_BTN_UP);
        }
    }

    void OnApplicationQuit()
    {
        ardThread.StopThread();
        //ardThread.CloseThread();
    }
}
