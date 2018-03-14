using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WindowsInput;

public class ArduinoManager : MonoBehaviour {

    //public ArduinoConnector ardCon;
    public ArduinoThread ardThread;
    private InputSimulator inputSim;

    // Use this for initialization
    void Start () {
        //ardCon = new ArduinoConnector();
        //ardCon.Open();
        ardThread = new ArduinoThread();
        ardThread.StartThread();
        inputSim = new InputSimulator();
    }

    // Update is called once per frame
    void Update()
    {
        // If user presses p, send PING
        if (Input.GetKeyUp("p"))
        {
            ardThread.SendToArduino("PING");
        }
        string fromArduino = ardThread.ReadFromArduino();
        if (fromArduino == "a1r")
        {
            //Messenger.Broadcast(GameEvent.A1_RED);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_1);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_1);
        }
        if (fromArduino == "a1b")
        {
            //Messenger.Broadcast(GameEvent.A1_BLUE);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_2);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_2);
        }
        if (fromArduino == "a2r")
        {
            //Messenger.Broadcast(GameEvent.A2_RED);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_3);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_3);
        }
        if (fromArduino == "a2b")
        {
            //Messenger.Broadcast(GameEvent.A2_BLUE);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_4);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_4);
        }
        if (fromArduino == "a3r")
        {
            //Messenger.Broadcast(GameEvent.A3_RED);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_5);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_5);
        }
        if (fromArduino == "a3b")
        {
            //Messenger.Broadcast(GameEvent.A3_BLUE);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_6);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_6);
        }
        if (fromArduino == "a4r")
        {
            //Messenger.Broadcast(GameEvent.A4_RED);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_7);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_7);
        }
        if (fromArduino == "a4b")
        {
            //Messenger.Broadcast(GameEvent.A4_BLUE);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_8);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_8);
        }
        if (fromArduino == "a5r")
        {
            //Messenger.Broadcast(GameEvent.A5_RED);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_9);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_9);

        }
        if (fromArduino == "a5b")
        {
            //Messenger.Broadcast(GameEvent.A5_BLUE);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_0);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_0);
        }
        if (fromArduino == "p1d")
        {
            //Messenger.Broadcast(GameEvent.P1_BTN_DOWN);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.LSHIFT);
        }
        if (fromArduino == "p1u")
        {
            //Messenger.Broadcast(GameEvent.P1_BTN_UP);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.LSHIFT);
        }
        if (fromArduino == "p2d")
        {
            //Messenger.Broadcast(GameEvent.P2_BTN_DOWN);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.RETURN);
        }
        if (fromArduino == "p2u")
        {
            //Messenger.Broadcast(GameEvent.P2_BTN_UP);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.RETURN);
        }
    }

    void OnApplicationQuit()
    {
        ardThread.StopThread();
        //ardThread.CloseThread();
    }
}
