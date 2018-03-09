using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRCarMovement : MonoBehaviour {

    public int playerNumber;
    [SerializeField] private float carMaxDistanceZ;
    [SerializeField] private float carSpeed = 1f;
    [SerializeField] private float driftSpeed = 1f;
    [SerializeField] private float driftDistance = 1f;
    [SerializeField] private float hoverSpeed = 6f;
    [SerializeField] private GameObject carExhaust;
    private bool playerKeyDown;
    private float newX;
    private float newY;
    private float newZ;
    private Vector3 startingPosition;

    private void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_REX_STARTING_POS, P1MoveToStartingPos);
        Messenger.AddListener(GameEvent.P2_REX_STARTING_POS, P2MoveToStartingPos);
    }

    // Use this for initialization
    void Start () {
        // Initialize values
        startingPosition = this.transform.localPosition;
        playerKeyDown = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        // Check to see if the player has traveled far enough to hit Mama Rex
        if (this.transform.localPosition.z > (startingPosition.z + carMaxDistanceZ))
        {
            // Trigger hit rex event
            if (playerNumber == 0)
            {
                Messenger.Broadcast(GameEvent.P1_HIT_REX);
            }
            else
            {
                Messenger.Broadcast(GameEvent.P2_HIT_REX);
            }
        }
        
        // Check to see if the player's key is down
        if ((playerNumber == 0 && Input.GetButton("RedPuppet")) || (playerNumber == 1 && Input.GetButton("BluePuppet"))) {
            playerKeyDown = true;
        } else
        {
            playerKeyDown = false;
        }

        // If the player's key isn't down, move laser car
        if (!playerKeyDown) {
            // Car exhaust on
            carExhaust.SetActive(true);

            // Update player's x value based on sin function
            float drift = (0.05f * driftDistance) * Mathf.Sin(driftSpeed * Time.time);
            newX = startingPosition.x + (drift * driftSpeed);

            // Update player's y value based on hover speed
            float hover = 0 + (0.1f * Mathf.Sin(hoverSpeed * Time.time));

            // Update player's z value each second if not stopped
            newZ = this.transform.localPosition.z + (carSpeed * .1f * Time.deltaTime);

            // Set new position for car
            this.transform.localPosition = new Vector3(newX, newY, newZ);
        } else
        {
            // Car exhaust off
            carExhaust.SetActive(false);

        }
    }

    void P1MoveToStartingPos()
    {
        MoveToStartingPos(0);
    }

    void P2MoveToStartingPos()
    {
        MoveToStartingPos(1);
    }

    void MoveToStartingPos(int playerNumber)
    {
        this.transform.localPosition = startingPosition;
    }
}
