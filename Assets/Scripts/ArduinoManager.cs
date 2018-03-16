using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WindowsInput;

public class ArduinoManager : MonoBehaviour {

    //public ArduinoConnector ardCon;
    private ArduinoThread ardThread;
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
        if (fromArduino == "a1rd")
        {
            //Messenger.Broadcast(GameEvent.A1_RED);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_1);
        }
        if (fromArduino == "a1ru")
        {
            //Messenger.Broadcast(GameEvent.A1_RED);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_1);
        }
        if (fromArduino == "a1bd")
        {
            //Messenger.Broadcast(GameEvent.A1_BLUE);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_2);
        }
        if (fromArduino == "a1bu")
        {
            //Messenger.Broadcast(GameEvent.A1_BLUE);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_2);
        }
        if (fromArduino == "a2rd")
        {
            //Messenger.Broadcast(GameEvent.A2_RED);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_3);
        }
        if (fromArduino == "a2ru")
        {
            //Messenger.Broadcast(GameEvent.A2_RED);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_3);
        }
        if (fromArduino == "a2bd")
        {
            //Messenger.Broadcast(GameEvent.A2_BLUE);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_4);
        }
        if (fromArduino == "a2bu")
        {
            //Messenger.Broadcast(GameEvent.A2_BLUE);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_4);
        }
        if (fromArduino == "a3rd")
        {
            //Messenger.Broadcast(GameEvent.A3_RED);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_5);
        }
        if (fromArduino == "a3ru")
        {
            //Messenger.Broadcast(GameEvent.A3_RED);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_5);
        }
        if (fromArduino == "a3bd")
        {
            //Messenger.Broadcast(GameEvent.A3_BLUE);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_6);
        }
        if (fromArduino == "a3bu")
        {
            //Messenger.Broadcast(GameEvent.A3_BLUE);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_6);
        }
        if (fromArduino == "a4rd")
        {
            //Messenger.Broadcast(GameEvent.A4_RED);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_7);
        }
        if (fromArduino == "a4ru")
        {
            //Messenger.Broadcast(GameEvent.A4_RED);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_7);
        }
        if (fromArduino == "a4bd")
        {
            //Messenger.Broadcast(GameEvent.A4_BLUE);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_8);
        }
        if (fromArduino == "a4bu")
        {
            //Messenger.Broadcast(GameEvent.A4_BLUE);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_8);
        }
        if (fromArduino == "a5rd")
        {
            //Messenger.Broadcast(GameEvent.A5_RED);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_9);
        }
        if (fromArduino == "a5ru")
        {
            //Messenger.Broadcast(GameEvent.A5_RED);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_9);

        }
        if (fromArduino == "a5bd")
        {
            //Messenger.Broadcast(GameEvent.A5_BLUE);
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_0);
        }
        if (fromArduino == "a5bu")
        {
            //Messenger.Broadcast(GameEvent.A5_BLUE);
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
        //ardThread.StopThread();
        //ardThread.CloseThread();
    }

    private void OnDestroy()
    {
        //ardThread.StopThread();
    }
}
