using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRCubeBehavior : MonoBehaviour {

    [SerializeField] private GameObject coinReference;
    [SerializeField] private int playerNumber;

    void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_CUBE_HIT, P1CubeHit);
        Messenger.AddListener(GameEvent.P2_CUBE_HIT, P2CubeHit);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CubeHit()
    {

    }

    private void P1CubeHit()
    {
        if (playerNumber == 0)
        {
            CubeHit();
        }
    }

    private void P2CubeHit()
    {
        if (playerNumber == 1)
        {
            CubeHit();
        }
    }
}
