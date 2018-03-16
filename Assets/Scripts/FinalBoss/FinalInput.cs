using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalInput : MonoBehaviour {

    [SerializeField] private GameObject[] laserAimsRed;
    [SerializeField] private GameObject[] laserAimsBlue;
    [SerializeField] private HealthBarController healthBar;

    void Awake()
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
        Messenger.AddListener(GameEvent.P1_BTN, P1Button);
        Messenger.AddListener(GameEvent.P2_BTN, P2Button);
    }

    void OnDestroy()
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

    // Main response logic to audience input
    private void AudienceInput(bool redInput, int audienceMemberNumber)
    {
        // Create Laser
        CreateAudienceLaser(audienceMemberNumber, redInput);

        // Subtract from health
        healthBar.DecreaseHealth();
    }

    private void CreateAudienceLaser(int audienceMemberOrPlayerNumber, bool redInput)
    {

        // Get a reference to the correct laser aim
        // Based on audience button press and player number

        FBLaserAimBehavior laserAimBehavior;

        if (redInput == true)
        {
            laserAimBehavior = laserAimsRed[audienceMemberOrPlayerNumber].GetComponent<FBLaserAimBehavior>();
        }
        else
        {
            laserAimBehavior = laserAimsBlue[audienceMemberOrPlayerNumber].GetComponent<FBLaserAimBehavior>();
        }

        // Call the laser aim's method to instantiate a new laser
        // Rotation/Position determined by each LaserAim
        laserAimBehavior.CreateNewLaser();
    }

    private void A1Red()
    {
        AudienceInput(true, 0);
    }

    private void A1Blue()
    {
        AudienceInput(false, 0);
    }

    private void A2Red()
    {
        AudienceInput(true, 1);
    }

    private void A2Blue()
    {
        AudienceInput(false, 1);
    }

    private void A3Red()
    {
        AudienceInput(true, 2);
    }

    private void A3Blue()
    {
        AudienceInput(false, 2);
    }

    private void A4Red()
    {
        AudienceInput(true, 3);
    }

    private void A4Blue()
    {
        AudienceInput(false, 3);
    }

    private void A5Red()
    {
        AudienceInput(true, 4);
    }

    private void A5Blue()
    {
        AudienceInput(false, 4);
    }

    private void P1Button()
    {
        AudienceInput(true, 5);
    }

    private void P2Button()
    {
        AudienceInput(false, 5);
    }
}
