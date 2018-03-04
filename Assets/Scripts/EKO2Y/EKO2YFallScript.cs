using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EKO2YFallScript : MonoBehaviour {

    [SerializeField] private GameObject deathPivot;
    [SerializeField] private GameObject playerHoldingImage;
    private float startedLaunchTime;
    [SerializeField] private float pauseTimeAfterLaunch = 5000;
    [SerializeField] private float blinkTimesPerSecond = 4;
    [SerializeField] private GameObject spriteRenderer;
    private Rigidbody2D m_Rigidbody2D;
    private UnityStandardAssets._2D.PlatformerCharacter2D platformerChar;
    
    public enum OccilationFuntion { Sine, Cosine }

    private enum State { inFall, inHolding, inLaunch, none }
    private bool red;

    private State state;

    private Collider2D[] colliders;
    private Rigidbody2D rigidBody;

    private Vector3 StartPosition;
    public Vector3 HoldingPosition;
    public float FallTime = 1.0f;
    private float StartTime;

    void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_RELEASE, P1Release);
        Messenger.AddListener(GameEvent.P2_RELEASE, P2Release);

        if (tag == "Player1")
        {
            red = false;
        } else if (tag == "Player2")
        {
            red = true;
        } else
        {
            Debug.LogError("Fall script attached to object that is not either player");
        }

        colliders = GetComponents<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        state = State.none;
        TurnOnChildRenderers(playerHoldingImage, false);
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        platformerChar = GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Player collides with block
        if (other.tag == "Block")
        {
            state = State.inFall;
            EnableColliders(false);
            rigidBody.bodyType = RigidbodyType2D.Static;
            StartPosition = transform.localPosition;
            StartTime = Time.time;
        }
        // Player collides with Bunnies
        if (other.tag == "Enemies")
        {
            // Transition from fall state to hold state 
            state = State.inHolding;

            // Move player offscreen
            transform.rotation.Set(0, 0, 0, 0);
            transform.position = new Vector3(-15f, 1.5f, 7f);


        }
    }

    private void EnableColliders(bool enabled)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = enabled;
        }
    }

    void Update()
    {
    //    // Runs every frame:
    //    // Three phases:
    //    // 1. inFall: Player has collided with an object; moving offscreen
    //    // 2. inHolding: Player is offscreen, waiting to be revived by audience
    //    // 3. inLaunch: Audience has released player; launching back to playfield
        if (state == State.inFall)
        {
            if (Time.time < StartTime + FallTime)
            {
                transform.localPosition = Vector3.Lerp(StartPosition, HoldingPosition, (Time.time - StartTime) / FallTime);
            } else
            {
                state = State.inHolding;

                // Begin voting
                if (red)
                {
                    Messenger.Broadcast(GameEvent.P2_ALL_RED);
                }
                else
                {
                    Messenger.Broadcast(GameEvent.P1_ALL_BLUE);
                }
            }
            //    // Else, rotate player towards bunnies
            //    transform.RotateAround(new Vector3(deathPivot.transform.position.x, deathPivot.transform.position.y + .8f, deathPivot.transform.position.z), Vector3.forward, 300 * Time.deltaTime);
        }

        else if (state == State.inLaunch)
        {
            //Debug.Log("in launch");
            if (transform.position.x < 0)
            {
    
                // Move to starting position
                if (red)
                {
                    transform.position = new Vector3(0.04f, 3f, 7f);
                } else
                {
                    transform.position = new Vector3(1.65f, 3f, 7f);
                }

                // Allow user to double jump
                platformerChar.SetDoubleJump(true);

            EnableColliders(true);
            rigidBody.bodyType = RigidbodyType2D.Dynamic;

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
    }

    private void P1Release()
    {
        if (tag == "Player1")
        {
            Release();
        }
    }

    private void P2Release()
    {
        if (tag == "Player2")
        {
            Release();
        }
    }

    private void Release()
    {
        // player is transitioning from holding to launch
        state = State.inLaunch;
        startedLaunchTime = Time.time;
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
        state = State.none;
    }
}
