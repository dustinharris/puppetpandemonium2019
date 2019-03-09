﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRLevelProgress : MonoBehaviour {

    [SerializeField] private GameObject[] rexLives;
    private int livesRemaining;
    private int subtractLifeIndex;

    private void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.REX_SUBTRACT_LIFE, SubtractLife);
    }

    // Use this for initialization
    void Start () {
        // Initialize Values
        livesRemaining = rexLives.Length;
        //Debug.Log("Total rex lives: " + livesRemaining);
	}

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.REX_SUBTRACT_LIFE, SubtractLife);
    }

    private void SubtractLife()
    {
        Debug.Log("Hit subtract life");

        // Get index for life bar that you want to destroy
        subtractLifeIndex = livesRemaining - 1;

        if (subtractLifeIndex >= 0)
        {
            // Destroy game object representing rex life
            Destroy(rexLives[subtractLifeIndex]);
        }

        // Update remaining lives count
        livesRemaining -= 1;

        Debug.Log("Lives remaining: " + livesRemaining);

        if (livesRemaining <= 0)
        {
            // Rex is defeated. End level.
            Debug.Log("Rex is defeated");
            this.GetComponent<LREndSequence>().RexInitiateEndSequence();
        }
    }
}
