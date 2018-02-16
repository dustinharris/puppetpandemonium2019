using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBroadcaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
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

        if (Input.GetButtonDown("Audience4Red"))
        {
            Messenger.Broadcast(GameEvent.A5_RED);
        }

        if (Input.GetButtonDown("Audience4Blue"))
        {
            Messenger.Broadcast(GameEvent.A5_BLUE);
        }
    }
}
