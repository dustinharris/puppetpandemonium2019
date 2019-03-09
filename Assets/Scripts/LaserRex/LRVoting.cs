using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRVoting : MonoBehaviour {

    [SerializeField] private GameObject laserCubeRed;
    [SerializeField] private GameObject laserCubeBlue;
    [SerializeField] private GameObject[] laserAimsRed;
    [SerializeField] private GameObject[] laserAimsBlue;
    private bool lasersEnabled = false;

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
        Messenger.AddListener(GameEvent.REX_START_SCENERY, RexEnableAudienceLasers);
        Messenger.AddListener(GameEvent.REX_STOP_SCENERY, RexDisableAudienceLasers);
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
        Messenger.RemoveListener(GameEvent.REX_DISABLE_AUDIENCE_LASERS, RexDisableAudienceLasers);

        Messenger.RemoveListener(GameEvent.GAME_START, RexEnableAudienceLasers);
        Messenger.RemoveListener(GameEvent.REX_START_SCENERY, RexEnableAudienceLasers);
        Messenger.RemoveListener(GameEvent.REX_STOP_SCENERY, RexDisableAudienceLasers);
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
