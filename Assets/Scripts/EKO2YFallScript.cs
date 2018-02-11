using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EKO2YFallScript : MonoBehaviour {

    private bool inFall;
    private bool inHolding;
    private bool inLaunch;
    [SerializeField] private GameObject deathPivot;
    [SerializeField] private GameObject playerHoldingImage;
    private bool keyDown;

    public enum OccilationFuntion { Sine, Cosine }

    void Start()
    {
        inFall = false;
        inHolding = false;
        inLaunch = false;
        keyDown = false;
        playerHoldingImage.GetComponent<Renderer>().enabled = false;
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
            inFall = false;
            inHolding = true;

            transform.rotation.Set(0, 0, 0, 0);
            transform.position = new Vector3(-15f, 1.5f, 7f);
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
                playerHoldingImage.GetComponent<Renderer>().enabled = false;

                // Move to starting position
                if (tag == "Player1")
                {
                    transform.position = new Vector3(1.65f, 3f, 7f);
                } else if (tag == "Player2")
                {
                    transform.position = new Vector3(0.04f, 3f, 7f);
                }
            }
            // For now, just throw back on screen.
            // TODO: Add animation
            else 
            {
                // Reset Rotation
                transform.rotation.Set(0, 0, 0, 0);
                inLaunch = false;
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
            playerHoldingImage.GetComponent<Renderer>().enabled = true;

            // Reset keydown
            keyDown = false;

            if (tag == "Player1")
            {
                keyDown = Input.GetKeyDown("l");
                //Debug.Log("inHolding: " + inHolding + "keyDown: " + keyDown);
            }
            else if (tag == "Player2")
            {
                keyDown = Input.GetKeyDown("a");
            }

            // If player can be launched, transition from inHolding to inLaunch
            if (inHolding && keyDown)
            {
                // player is in holding and trying to get out
                inHolding = false;
                inLaunch = true;
            }
        }
    }
}
