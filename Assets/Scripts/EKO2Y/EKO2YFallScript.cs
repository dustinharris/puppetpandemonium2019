using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EKO2YFallScript : MonoBehaviour {

    private bool inFall;
    private bool inHolding;
    private bool inLaunch;
    [SerializeField] private GameObject deathPivot;
    [SerializeField] private GameObject playerHoldingImage;
    private bool keyDown;
    private float startedLaunchTime;
    [SerializeField] private float pauseTimeAfterLaunch = 5000;
    [SerializeField] private float blinkTimesPerSecond = 4;
    [SerializeField] private GameObject spriteRenderer;
    private Rigidbody2D m_Rigidbody2D;
    private UnityStandardAssets._2D.PlatformerCharacter2D platformerChar;
    
    public enum OccilationFuntion { Sine, Cosine }

    void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_RELEASE, P1Release);
        Messenger.AddListener(GameEvent.P2_RELEASE, P2Release);
    }

    void Start()
    {
        inFall = false;
        inHolding = false;
        inLaunch = false;
        keyDown = false;
        TurnOnChildRenderers(playerHoldingImage, false);
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        platformerChar = GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Player collides with block
        if ((tag == "Player1" || tag == "Player2") && other.tag == "Block")
        {
            // Increase Z to avoid box colliders
            transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z - .5f);
            
            inFall = true;
        }
        // Player collides with Bunnies
        if ((tag == "Player1" || tag == "Player2") && other.tag == "Enemies")
        {
            // Transition from fall state to hold state 
            inFall = false;
            inHolding = true;

            // Move player offscreen
            transform.rotation.Set(0, 0, 0, 0);
            transform.position = new Vector3(-15f, 1.5f, 7f);

            // Begin voting
            if (tag == "Player1")
            {
                Messenger.Broadcast(GameEvent.P1_ALL_BLUE);
            }
            if (tag == "Player2")
            {
                Messenger.Broadcast(GameEvent.P2_ALL_RED);
            }

        }
    }

    void Update()
    {
        // Runs every frame:
        // Three phases:
        // 1. inFall: Player has collided with an object; moving offscreen
        // 2. inHolding: Player is offscreen, waiting to be revived by audience
        // 3. inLaunch: Audience has released player; launching back to playfield
        if (inFall)
        {
            if (transform.position.x < -4.5)
            {
                inFall = false;
                inHolding = true;
            }
            else if (transform.position.y < 3)
            {
                //Debug.Log("inFall;");
                // Else, rotate player towards bunnies
                transform.RotateAround(new Vector3(deathPivot.transform.position.x, deathPivot.transform.position.y + .8f, deathPivot.transform.position.z), Vector3.forward, 300 * Time.deltaTime);
                //other.transform.position = new Vector3(other.transform.position.x, Mathf.Sin(Time.time), other.transform.position.z);
            }
        }

        else if (inLaunch)
        {
            //Debug.Log("in launch");
            if (transform.position.x < 0)
            {
                // Hide player holding image
                TurnOnChildRenderers(playerHoldingImage, false);

                // Move to starting position
                if (tag == "Player1")
                {
                    transform.position = new Vector3(1.65f, 3f, 7f);
                } else if (tag == "Player2")
                {
                    transform.position = new Vector3(0.04f, 3f, 7f);
                }

                // Allow user to double jump
                platformerChar.SetDoubleJump(true);

                // Start blinking animation
                StartCoroutine(Blink(pauseTimeAfterLaunch));


            }
            // For now, just throw back on screen.
            // TODO: Add animation
            else 
            {
                // Reset Rotation
                transform.rotation = Quaternion.identity;

                // Hold y position for X seconds
                if ((Time.time - pauseTimeAfterLaunch) < startedLaunchTime)
                {
                    // Reset y velocity
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -1f);

                    //Debug.Log("Time: " + (Time.time - pauseTimeAfterLaunch) + "Started: " + startedLaunchTime);
                    // Hijack character position
                    this.gameObject.transform.position = new Vector3(transform.position.x, 2, transform.position.z);
                }
            }
        }

        else if (inHolding)
        {
            /**{
                player.transform.position = new Vector3(-15f, 1.5f, 7f);
                // TODO: Show holidng sprites
            }**/
            // Listen for "release" button based on player.
            // For now this is initiated by player button.
            // TODO: Convert to audience buttons

            // Show player holding image
            TurnOnChildRenderers(playerHoldingImage, true);

            // Reset keydown
            keyDown = false;

            if (tag == "Player1")
            {
                keyDown = Input.GetButtonDown("BluePuppet");
            }
            else if (tag == "Player2")
            {
                keyDown = Input.GetButtonDown("RedPuppet");
            }

            // If player can be launched, transition from inHolding to inLaunch
            if (inHolding && keyDown)
            {
                // player is in holding and trying to get out
                inHolding = false;
                inLaunch = true;
                startedLaunchTime = Time.time;
            }
        }
    }

    private void P1Release()
    {
        if (tag == "Player1")
        {
            // player is transitioning from holding to launch
            inHolding = false;
            inLaunch = true;
            startedLaunchTime = Time.time;
        }
    }

    private void P2Release()
    {
        if (tag == "Player2")
        {
            // player is transitioning from holding to launch
            inHolding = false;
            inLaunch = true;
            startedLaunchTime = Time.time;
        }
    }

    private void TurnOnChildRenderers(GameObject obj, bool turnOn)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            if (turnOn == true)
            {
                r.enabled = true;
            }
            if (turnOn == false)
            {
                r.enabled = false;
            }
        }
    }

    IEnumerator Blink(float delay)
    {
        int totalBlinkTimes = (int)(blinkTimesPerSecond * pauseTimeAfterLaunch);
        // Enable double jump
        for (int i = 0; i < totalBlinkTimes; i++)
        {
            if (i%2 == 0)
            {
                // Even -- blink on
                TurnOnChildRenderers(this.gameObject, true);
            }
            if (i%2 == 1)
            {
                // Odd - blink off
                TurnOnChildRenderers(this.gameObject, false);

            }
            // Delay until next blink time

            yield return new WaitForSeconds(1/blinkTimesPerSecond);
        }
        // Afterwards blinking ends, make sure that the character is rendering again.
        TurnOnChildRenderers(this.gameObject, true);
        
        // Launch ends
        inLaunch = false;
    }
}
