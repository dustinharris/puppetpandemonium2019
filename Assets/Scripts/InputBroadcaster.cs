using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;

public class InputBroadcaster : MonoBehaviour {

    private InputSimulator inputSim;

    void Start()
    {
        inputSim = new InputSimulator();
    }

    void Update () {
        if (Input.GetButtonDown("Audience0Red"))
        {
            Messenger.Broadcast(GameEvent.A1_RED);
        }

        if (Input.GetButtonDown("Audience0Blue"))
        {
            Messenger.Broadcast(GameEvent.A1_BLUE);
        }

        if (Input.GetButtonDown("Audience1Red"))
        {
            Messenger.Broadcast(GameEvent.A2_RED);
        }

        if (Input.GetButtonDown("Audience1Blue"))
        {
            Messenger.Broadcast(GameEvent.A2_BLUE);
        }

        if (Input.GetButtonDown("Audience2Red"))
        {
            Messenger.Broadcast(GameEvent.A3_RED);
        }

        if (Input.GetButtonDown("Audience2Blue"))
        {
            Messenger.Broadcast(GameEvent.A3_BLUE);
        }

        if (Input.GetButtonDown("Audience3Red"))
        {
            Messenger.Broadcast(GameEvent.A4_RED);
        }

        if (Input.GetButtonDown("Audience3Blue"))
        {
            Messenger.Broadcast(GameEvent.A4_BLUE);
        }

        if (Input.GetButtonDown("RedPuppet"))
        {
            //Messenger.Broadcast(GameEvent.P1_BTN);
        }

        if (Input.GetButtonDown("BluePuppet"))
        {
            //Messenger.Broadcast(GameEvent.P2_BTN);
        }

        //
        // The following two are used to test arduino code
        //
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("ADown");
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.LSHIFT);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("AUp");
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.LSHIFT);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("SDown");
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.RETURN);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("SUp");
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.RETURN);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D");
            inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_1);
            inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_1);
        }
    }
}
