using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRCarMovement : MonoBehaviour
{

    public int playerNumber;
    [SerializeField] private float carMaxDistanceZ;
    [SerializeField] private float carSpeed = 1f;
    [SerializeField] private float driftSpeed = 1f;
    [SerializeField] private float driftDistance = 1f;
    [SerializeField] private float hoverSpeed = 6f;
    [SerializeField] private GameObject carExhaust;

    private LRDrift drift;
    private bool playerKeyDown;
    private float newZ;
    private Vector3 startingPosition;

    private string ButtonName;

    private void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_REX_STARTING_POS, P1MoveToStartingPos);
        Messenger.AddListener(GameEvent.P2_REX_STARTING_POS, P2MoveToStartingPos);

        if (playerNumber == 0)
        {
            ButtonName = "RedPuppet";
        }
        else
        {
            ButtonName = "BluePuppet";
        }
    }

    // Use this for initialization
    void Start()
    {
        // Initialize values
        startingPosition = this.transform.localPosition;
        playerKeyDown = false;
        drift = GetComponent<LRDrift>();
    }

    // Update is called once per frame
    void Update()
    {

        // If button was released this frame
        bool buttonReleased = false;
        // If button was pressed this frame
        bool buttonPressed = false;

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

        if (Input.GetButton(ButtonName))
        {
            playerKeyDown = true;
            if (Input.GetButtonDown(ButtonName))
            {
                buttonPressed = true;
            }
        }
        else
        {
            playerKeyDown = false;
            if (Input.GetButtonUp(ButtonName))
            {
                buttonReleased = true;
            }
        }

        if (buttonPressed)
        {
            // Car exhaust on
            carExhaust.SetActive(false);
            drift.Stop();
        }
        if (buttonReleased)
        {
            // Car exhaust off
            carExhaust.SetActive(true);
            drift.Resume();
        }


        // If the player's key isn't down, move laser car
        if (!playerKeyDown)
        {
            // Update player's z value each second if not stopped
            newZ = this.transform.localPosition.z + (carSpeed * .1f * Time.deltaTime);

            // Set new position for car
            this.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newZ);
        }
    }

    void P1MoveToStartingPos()
    {
        if (playerNumber == 0)
        {
            MoveToStartingPos(0);
        }
    }

    void P2MoveToStartingPos()
    {
        if (playerNumber == 1)
        {
            MoveToStartingPos(1);
        }
    }

    void MoveToStartingPos(int playerNumber)
    {
        this.transform.localPosition = startingPosition;
    }
}
