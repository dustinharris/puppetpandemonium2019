using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRVoting : MonoBehaviour {

    [SerializeField] private GameObject laserCubeRed;
    [SerializeField] private GameObject laserCubeBlue;
    [SerializeField] private GameObject[] laserAimsRed;
    [SerializeField] private GameObject[] laserAimsBlue;
    public int laserCubeRedHealth = 100;
    public int laserCubeBlueHealth = 100;
    private int[] healthArray;
    private bool A1RedState;
    private bool A1BlueState;
    private bool A2RedState;
    private bool A2BlueState;
    private bool A3RedState;
    private bool A3BlueState;
    private bool A4RedState;
    private bool A4BlueState;
    private bool A5RedState;
    private bool A5BlueState;
    private bool lasersEnabled = true;

    private void Awake()
    {
        // Listen for audience-triggered events
        Messenger.AddListener(GameEvent.A1_RED, A1Red);
        Messenger.AddListener(GameEvent.A1_BLUE, A1Blue);
        Messenger.AddListener(GameEvent.A2_RED, A2Red);
        Messenger.AddListener(GameEvent.A2_BLUE, A2Blue);
        Messenger.AddListener(GameEvent.A3_RED, A3Red);
        Messenger.AddListener(GameEvent.A3_BLUE, A3Blue);
        Messenger.AddListener(GameEvent.A4_RED, A4Red);
        Messenger.AddListener(GameEvent.A4_BLUE, A4Blue);
        Messenger.AddListener(GameEvent.A5_RED, A5Red);
        Messenger.AddListener(GameEvent.A5_BLUE, A5Blue);


        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.REX_DISABLE_AUDIENCE_LASERS, RexDisableAudienceLasers);

        Messenger.AddListener(GameEvent.GAME_START, RexEnableAudienceLasers);
    }

    private void OnDestroy()
    {
        // Destroy broadcast listeners created by this object
        Messenger.RemoveListener(GameEvent.A1_RED, A1Red);
        Messenger.RemoveListener(GameEvent.A1_BLUE, A1Blue);
        Messenger.RemoveListener(GameEvent.A2_RED, A2Red);
        Messenger.RemoveListener(GameEvent.A2_BLUE, A2Blue);
        Messenger.RemoveListener(GameEvent.A3_RED, A3Red);
        Messenger.RemoveListener(GameEvent.A3_BLUE, A3Blue);
        Messenger.RemoveListener(GameEvent.A4_RED, A4Red);
        Messenger.RemoveListener(GameEvent.A4_BLUE, A4Blue);
        Messenger.RemoveListener(GameEvent.A5_RED, A5Red);
        Messenger.RemoveListener(GameEvent.A5_BLUE, A5Blue);
    }

    // Use this for initialization
    void Start () {
        // Initialize audience button state booleans
        A1RedState = false;
        A1BlueState = false;
        A2RedState = false;
        A2BlueState = false;
        A3RedState = false;
        A3BlueState = false;
        A4RedState = false;
        A4BlueState = false;
        A5RedState = false;
        A5BlueState = false;

        // Create array of integers for current health
        healthArray = new int[2];
        healthArray[0] = laserCubeRedHealth;
        healthArray[1] = laserCubeBlueHealth;

        RexDisableAudienceLasers();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Main response logic to audience input
    private void LRAudienceAction(int audienceMemberNumber, int playerNumber)
    {
        // Check if in pause state -- TODO
        if(lasersEnabled)
        {
            // Create Laser
            CreateAudienceLaser(audienceMemberNumber, playerNumber);

            // For targeted player's laser cube:
            // If laser cube health > 0, subtract 1

            // Trigger coin animation
            if (playerNumber == 0)
            {
                Messenger.Broadcast(GameEvent.P1_CUBE_HIT);
            }
            else
            {
                Messenger.Broadcast(GameEvent.P2_CUBE_HIT);
            }

            if (healthArray[playerNumber] > 0)
            {
                // Subtract 1
                healthArray[playerNumber] -= 1;

                // Show coin animation
                // Todo
            }
            else
            {
                // Trigger distraction state
                // Todo
            }
        }
    }

    private void CreateAudienceLaser(int audienceMemberNumber, int playerNumber)
    {

        // Get a reference to the correct laser aim
        // Based on audience button press and player number

        LRLaserAimBehavior laserAimBehavior;

        // Subtract one from audience member to access correct place in the array
        int arrayCorrectedAudienceMemberNumber = audienceMemberNumber - 1;

        if (playerNumber == 0)
        {
            laserAimBehavior = laserAimsRed[arrayCorrectedAudienceMemberNumber].GetComponent<LRLaserAimBehavior>();
        }
        else
        {
            laserAimBehavior = laserAimsBlue[arrayCorrectedAudienceMemberNumber].GetComponent<LRLaserAimBehavior>();
        }

        // Call the laser aim's method to instantiate a new laser
        // Rotation/Position determined by each LaserAim
        laserAimBehavior.CreateNewLaser();

    }

    private void RexDisableAudienceLasers()
    {
        Debug.Log("Trying to disable lasers");
        lasersEnabled = false;
    }

    private void RexEnableAudienceLasers()
    {
        lasersEnabled = true;
    }

    private void A1Red()
    {
        LRAudienceAction(1, 0);
    }

    private void A1Blue()
    {
        LRAudienceAction(1, 1);
    }

    private void A2Red()
    {
        LRAudienceAction(2, 0);
    }

    private void A2Blue()
    {
        LRAudienceAction(2, 1);
    }

    private void A3Red()
    {
        LRAudienceAction(3, 0);
    }

    private void A3Blue()
    {
        LRAudienceAction(3, 1);
    }

    private void A4Red()
    {
        LRAudienceAction(4, 0);
    }

    private void A4Blue()
    {
        LRAudienceAction(4, 1);
    }


    private void A5Red()
    {
        LRAudienceAction(5, 0);
    }

    private void A5Blue()
    {
        LRAudienceAction(5, 1);
    }
}
