using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateInitializer : MonoBehaviour
{

    /*
     * Attach to game object to initialize static game state
     * 
     */

    void Awake()
    {
        GameState.Initialize();
    }
}