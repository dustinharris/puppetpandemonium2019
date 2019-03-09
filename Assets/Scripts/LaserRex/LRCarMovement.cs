﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRCarMovement : MonoBehaviour
{

    public int playerNumber;
    [SerializeField] private float carMaxDistanceZ;
    [SerializeField] private float carSpeed = 1f;
    [SerializeField] private GameObject carExhaust;
    [SerializeField] private GameObject carStoppedIcon;
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private float invincibilityBlinkInterval = .2f;
    [SerializeField] private GameObject rexObject;
    [SerializeField] private GameObject otherPlayer;
    [SerializeField] private float hitTieBreakerPenalty = .05f;
    private bool carStopped;

    private LRDrift drift;
    private LRRexBehavior rexBehavior;
    private bool playerKeyDown;
    private float newZ;
    private Vector3 startingPosition;
    private Vector3 hitRexPosition;
    private bool carInvinvincible;
    private Renderer carRenderer;
    private bool rexDefeated;
    private bool gameStarted = false;
    private bool paused = false;
    private bool rexInHitState = false;

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
        Messenger.AddListener(GameEvent.REX_STOP_SCENERY, Pause);
        Messenger.AddListener(GameEvent.REX_START_SCENERY, Unpause);

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
        carRenderer = this.GetComponent<Renderer>();
        rexDefeated = false;
        carExhaust.SetActive(true);
        rexBehavior = rexObject.GetComponent<LRRexBehavior>();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.P1_REX_STARTING_POS, P1MoveToStartingPos);
        Messenger.RemoveListener(GameEvent.P2_REX_STARTING_POS, P2MoveToStartingPos);
        Messenger.RemoveListener(GameEvent.REX_P1_START_INVINCIBILITY, RexP1StartInvincibility);
        Messenger.RemoveListener(GameEvent.REX_P2_START_INVINCIBILITY, RexP2StartInvincibility);
        Messenger.RemoveListener(GameEvent.REX_P1_STOP_INVINCIBILITY, RexP1StopInvincibility);
        Messenger.RemoveListener(GameEvent.REX_P2_STOP_INVINCIBILITY, RexP2StopInvincibility);
        Messenger.RemoveListener(GameEvent.REX_DEFEATED, RexDefeated);
        Messenger.RemoveListener(GameEvent.GAME_START, GameStarted);
        Messenger.RemoveListener(GameEvent.REX_STOP_SCENERY, Pause);
        Messenger.RemoveListener(GameEvent.REX_START_SCENERY, Unpause);
    }

    private void GameStarted()
    {
        gameStarted = true;

        ButtonPressed();

        if (playerNumber == 0)
        {
            Messenger.Broadcast(GameEvent.REX_P1_START_INVINCIBILITY);
        }
        else
        {
            Messenger.Broadcast(GameEvent.REX_P2_START_INVINCIBILITY);
        }
    }

    private void Pause()
    {
        paused = true;
    }

    private void Unpause()
    {
        paused = false;
        if (Input.GetButton(ButtonName))
        {
            ButtonPressed();
        }
        else
        {
            ButtonReleased();
        }
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
                // Get other character z distance
                Vector3 otherCharacterDistance = otherPlayer.GetComponent<LRCarMovement>().getCarEndPosition();
                float otherCharacterZDistance = otherCharacterDistance.z;

                // Hit rex tie breaker logic:
                // If other character is first player and their Z is >= this player's Z
                // 1. Apply penalty to player two
                // 2. Don't trigger event below

                if (playerNumber == 1 && (otherPlayer.transform.position.z >= this.transform.position.z))
                {
                    // Apply Z distance penalty to player two
                    // This will prevent tie edge conditions on hit rex
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - hitTieBreakerPenalty));
                } else
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
                ButtonPressed();
            }
            if (buttonReleased)
            {
                ButtonReleased();
            }

            // Check whether Rex is currently in hit state.
            // If so, we won't want to move the car forward below.
            rexInHitState = rexBehavior.GetRexInHitState();

            // If the player's key isn't down && not invincible && not in end sequence, move forward
            if (!playerKeyDown && !carInvinvincible && !rexDefeated && !rexInHitState)
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

    private void ButtonPressed()
    {
        StopCar();

        // Show car stopped icon
        if (!paused)
        {
            carStoppedIcon.SetActive(true);
        }

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

    private void ButtonReleased()
    {
        // Car exhaust off
        carExhaust.SetActive(true);
        drift.Resume();

        // Don't show car stopped icon
        if (!paused)
        {
            carStoppedIcon.SetActive(false);
        }
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
        MoveToStartingPos(playerNumber);
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
        float blinkStopTime = blinkStartTime + waitTime / 2;

        while (Time.time < blinkStopTime)
        {
            // Hide car
            carRenderer.enabled = false;

            // Wait for blink interval
            yield return new WaitForSeconds(invincibilityBlinkInterval);

            // Show car
            carRenderer.enabled = true;

            // Wait for blink interval
            yield return new WaitForSeconds(invincibilityBlinkInterval);
        }

        // Afterwards, make sure car is visible
        carRenderer.enabled = true;

        yield return new WaitForSeconds(waitTime / 2);

        // Turn off invincibility
        if (playerNumber == 0)
        {
            Messenger.Broadcast(GameEvent.REX_P1_STOP_INVINCIBILITY);
        }
        else
        {
            Messenger.Broadcast(GameEvent.REX_P2_STOP_INVINCIBILITY);
        }
    }
}