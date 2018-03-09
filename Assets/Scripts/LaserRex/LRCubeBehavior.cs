using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRCubeBehavior : MonoBehaviour {

    [SerializeField] private int playerNumber;

    void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_CUBE_HIT, P1CubeHit);
        Messenger.AddListener(GameEvent.P2_CUBE_HIT, P2CubeHit);
    }

    private void CubeHitByLaser()
    {

    }
   
    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    private void P1CubeHit()
    {
        if (playerNumber == 0)
        {
            Messenger.Broadcast(GameEvent.P1_CUBE_NEW_COIN);
            CubeHitByLaser();
        }
    }

    private void P2CubeHit()
    {
        if (playerNumber == 1)
        {
            Messenger.Broadcast(GameEvent.P2_CUBE_NEW_COIN);
            CubeHitByLaser();
        }
    }
}
