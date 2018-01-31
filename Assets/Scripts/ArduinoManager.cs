using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArduinoManager : MonoBehaviour {

    //public ArduinoConnector ardCon;
    public ArduinoThread ardThread;

    private bool p1jump = false;
    private bool p2jump = false;
   
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
        if (fromArduino == "p1")
        {
            // Make p1 jump
            p1jump = true;
        }
        if (fromArduino == "p2")
        {
            // Make p2 jump
            Debug.Log("P2 JUMP TRUE");
            p2jump = true;
        }
    }

    public bool readP1Jump()
    {
        return p1jump;
    }

    public void endP1Jump()
    {
        p1jump = false;
    }

    public bool readP2Jump()
    {
        return p2jump;
    }

    public void endP2Jump()
    {
        p2jump = false;
    }

    void OnApplicationQuit()
    {
        ardThread.StopThread();
        //ardThread.CloseThread();
    }
}
