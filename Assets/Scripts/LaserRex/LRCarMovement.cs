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
    [SerializeField] private GameObject carStoppedIcon;
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private float invincibilityBlinkInterval = .2f;
    private bool carStopped;

    private LRDrift drift;
    private bool playerKeyDown;
    private float newZ;
    private Vector3 startingPosition;
    private Vector3 hitRexPosition;
    private bool carInvinvincible;
    private Renderer carRenderer;
    private bool rexDefeated;
    private bool gameStarted = false;

    private string ButtonName;

    private void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_REX_STARTING_POS, P1MoveToStartingPos);
        Messenger.AddListener(GameEvent.P2_REX_STARTING_POS, P2MoveToStartingPos);
        Messenger.AddListener(GameEvent.REX_P1_START_INVINCIBILITY, RexP1StartInvincibility);
        Messenger.AddListener(GameEvent.REX_P2_START_INVINCIBILITY, RexP2StartInvincibility);
        Messenger.AddListener(GameEvent.REX_P1_STOP_INVINCIBILITY, RexP1StopInvincibility);
        Messenger.AddListener(GameEvent.REX_P2_STOP_INVINCIBILITY, RexP2StopInvincibility);
        Messenger.AddListener(GameEvent.REX_DEFEATED, RexDefeated);
        Messenger.AddListener(GameEvent.GAME_START, GameStarted);

        if (playerNumber == 0)
        {
            ButtonName = "RedPuppet";
        }
        else
        {
            ButtonName = "BluePuppet";
        }

        // Need to happen early so they can be called in Timeline script's Start function
        startingPosition = this.transform.localPosition;
        hitRexPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, (float)(this.transform.localPosition.z + carMaxDistanceZ));
    }

    // Use this for initialization
    void Start()
    {
        // Initialize values
        playerKeyDown = false;
        drift = GetComponent<LRDrift>();
        carStopped = false;
        carRenderer = this.GetComponent<MeshRenderer>();
        rexDefeated = false;
        carExhaust.SetActive(false);
        
        if (playerNumber == 0)
        {
            Messenger.Broadcast(GameEvent.REX_P1_START_INVINCIBILITY);
        } else
        {
            Messenger.Broadcast(GameEvent.REX_P2_START_INVINCIBILITY);
        }
    }

    private void GameStarted()
    {
        gameStarted = true;
        carExhaust.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted && !rexDefeated)
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
                StopCar();

                // Show car stopped icon
                carStoppedIcon.SetActive(true);

                // Broadcast player stopped moving event
                if (playerNumber == 0)
                {
                    Messenger.Broadcast(GameEvent.REX_P1_STOP_MOVING);
                }
                else
                {
                    Messenger.Broadcast(GameEvent.REX_P2_STOP_MOVING);
                }
            }
            if (buttonReleased)
            {
                // Car exhaust off
                carExhaust.SetActive(true);
                drift.Resume();

                // Don't show car stopped icon
                carStoppedIcon.SetActive(false);
                carStopped = false;

                // Broadcast player started moving event
                if (playerNumber == 0)
                {
                    Messenger.Broadcast(GameEvent.REX_P1_START_MOVING);
                }
                else
                {
                    Messenger.Broadcast(GameEvent.REX_P2_START_MOVING);
                }
            }

            // If the player's key isn't down && not invincible && not in end sequence, move forward
            if (!playerKeyDown && !carInvinvincible && !rexDefeated)
            {
                // Update player's z value each second if not stopped
                newZ = this.transform.localPosition.z + (carSpeed * .1f * Time.deltaTime);

                // Set new position for car
                this.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newZ);
            }
        }
    }

    private void StopCar()
    {
        // Car exhaust off
        carExhaust.SetActive(false);
        drift.Stop();

        carStopped = true;
    }

    private void setInvincibility(int playerNum, bool startInvincibility)
    {
        // Set car invincible state
        carInvinvincible = startInvincibility;

        // If car is now invincible, start start blinking coroutine.
        if (carInvinvincible)
        {
            StartCoroutine(CarBlink(invincibilityDuration));
        }
    }

    private void RexDefeated()
    {
        rexDefeated = true;
        StopCar();
    }

    private void RexP1StartInvincibility()
    {
        if (playerNumber == 0)
        {
            setInvincibility(playerNumber, true);
        }
    }

    private void RexP2StartInvincibility()
    {
        if (playerNumber == 1)
        {
            setInvincibility(playerNumber, true);
        }
    }

    private void RexP1StopInvincibility()
    {
        if (playerNumber == 0)
        {
            setInvincibility(playerNumber, false);
        }
    }

    private void RexP2StopInvincibility()
    {
        if (playerNumber == 1)
        {
            setInvincibility(playerNumber, false);
        }
    }

    public Vector3 getCarStartingPosition()
    {
        return startingPosition;
    }

    public Vector3 getCarEndPosition()
    {
        return hitRexPosition;
    }

    void P1MoveToStartingPos()
    {
        if (playerNumber == 0)
        {
            // Move player to starting position
            MoveToStartingPos(0);
            // Make player temporarily invincible
            Messenger.Broadcast(GameEvent.REX_P1_START_INVINCIBILITY);
        }
    }

    void P2MoveToStartingPos()
    {
        if (playerNumber == 1)
        {
            // Move player to starting position
            MoveToStartingPos(1);
            // Make player temporarily invincible
            Messenger.Broadcast(GameEvent.REX_P2_START_INVINCIBILITY);
        }
    }

    void MoveToStartingPos(int playerNumber)
    {
        this.transform.localPosition = startingPosition;
    }

    private IEnumerator CarBlink(float waitTime)
    {
        float blinkStartTime = Time.time;
        float blinkStopTime = blinkStartTime + waitTime/2;

        while (Time.time < blinkStopTime)
        {
            // Hide car
            this.GetComponent<Renderer>().enabled = false;

            // Wait for blink interval
            yield return new WaitForSeconds(invincibilityBlinkInterval);

            // Show car
            this.GetComponent<Renderer>().enabled = true;

            // Wait for blink interval
            yield return new WaitForSeconds(invincibilityBlinkInterval);
        }

        // Afterwards, make sure car is visible
        this.GetComponent<Renderer>().enabled = true;

        yield return new WaitForSeconds(waitTime / 2);

        // Turn off invincibility
        if (playerNumber == 0)
        {
            Messenger.Broadcast(GameEvent.REX_P1_STOP_INVINCIBILITY);
        } else
        {
            Messenger.Broadcast(GameEvent.REX_P2_STOP_INVINCIBILITY);
        }
    }
}
