using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRHitRex : MonoBehaviour {

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera hitCamera;

    void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_HIT_REX, P1HitRex);
        Messenger.AddListener(GameEvent.P2_HIT_REX, P2HitRex);
    }
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void P1HitRex()
    {
        //Debug.Log("P1 hit rex");
        // Call main hit method with player number = 0
        HitRex(0);

        // Broadcast message to reset P1 position to starting point
        Messenger.Broadcast(GameEvent.P1_REX_STARTING_POS);
    }

    private void P2HitRex()
    {
        //Debug.Log("P2 hit rex");
        // Call main hit method with player number = 1
        HitRex(1);

        // Broadcast message to reset P2 position to starting point
        Messenger.Broadcast(GameEvent.P2_REX_STARTING_POS);
    }

    private void HitRex(int playerNumber)
    {
        // Disable main camera, enable hit camera
        //hitCamera.gameObject.SetActive(true);
        //mainCamera.gameObject.SetActive(false);

        // Show jumping animation

        // Subtract Rex life
        Messenger.Broadcast(GameEvent.REX_SUBTRACT_LIFE);
    }
}
