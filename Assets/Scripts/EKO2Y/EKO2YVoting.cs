using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EKO2YVoting : MonoBehaviour {

    [SerializeField] private AudienceBarScript audienceBarScript;
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
    private bool redInVotingState;
    private bool blueInVotingState;

    void Awake()
    {
        // Listen for game-triggered events
        Messenger.AddListener(GameEvent.P1_ALL_BLUE, P1AllBlue);
        Messenger.AddListener(GameEvent.P2_ALL_RED, P2AllRed);

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
    }
    
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.P1_ALL_BLUE, P1AllBlue);
        Messenger.RemoveListener(GameEvent.P2_ALL_RED, P2AllRed);
    }

    // Use this for initialization
    void Start () {
        // Get reference to bar script to set display
        //AudienceBarScript audienceBarScript = GetComponent<AudienceBarScript>();

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

        // Initialize voting state booleans
        redInVotingState = false;
        blueInVotingState = false;
    }

    private void Update()
    {
        // Check to see if blue character is currently in voting state
        if (blueInVotingState)
        {
            // If all audience members have voted for blue, release holding state
            if (A1BlueState && A2BlueState && A3BlueState && A4BlueState && A5BlueState)
            {
                // Broadcast Message to release P2
                Messenger.Broadcast(GameEvent.P1_RELEASE);

                // Set all audience variables to false
                ResetBlueVotingVariables();

                // Take blue out of voting state
                blueInVotingState = false;
            }
        }
        // Check to see if red character is currently in voting state
        if (redInVotingState)
        {
            // If all audience members have voted for red, release holding state
            if (A1RedState && A2RedState && A3RedState && A4RedState && A5RedState)
            {
                // Broadcast Message to release P1
                Messenger.Broadcast(GameEvent.P2_RELEASE);

                // Set all audience variables to false
                ResetRedVotingVariables();

                // Take red out of voting state
                redInVotingState = false;
            }
        } 
    }
    
    private void P1AllBlue()
    {
        // Game requests blue votes from all audience members
        blueInVotingState = true;

        // Show all Blue
        audienceBarScript.ShowAll(AudienceUIScript.Notice.Alert, false);
    }

    private void P2AllRed()
    {
        // Game requests red votes from all audience members
        redInVotingState = true;

        // Show all Red 
        audienceBarScript.ShowAll(AudienceUIScript.Notice.Alert, true);
    }

    private void A1Red()
    {
        // Set variable to true
        A1RedState = true;

        // Replace alert with correct check mark
        audienceBarScript.Show(0, AudienceUIScript.Notice.Correct, true);
    }

    private void A1Blue()
    {
        // Set variable to true
        A1BlueState = true;

        // Replace alert with correct check mark
        audienceBarScript.Show(0, AudienceUIScript.Notice.Correct, false);
    }

    private void A2Red()
    {
        // Set variable to true
        A2RedState = true;

        // Replace alert with correct check mark
        audienceBarScript.Show(1, AudienceUIScript.Notice.Correct, true);
    }

    private void A2Blue()
    {
        // Set variable to true
        A2BlueState = true;

        // Replace alert with correct check mark
        audienceBarScript.Show(1, AudienceUIScript.Notice.Correct, false);
    }
    
    private void A3Red()
    {
        // Set variable to true
        A3RedState = true;

        // Replace alert with correct check mark
        audienceBarScript.Show(2, AudienceUIScript.Notice.Correct, true);
    }

    private void A3Blue()
    {
        // Set variable to true
        A3BlueState = true;

        // Replace alert with correct check mark
        audienceBarScript.Show(2, AudienceUIScript.Notice.Correct, false);
    }

    private void A4Red()
    {
        // Set variable to true
        A4RedState = true;

        // Replace alert with correct check mark
        audienceBarScript.Show(3, AudienceUIScript.Notice.Correct, true);
    }

    private void A4Blue()
    {
        // Set variable to true
        A4BlueState = true;

        // Replace alert with correct check mark
        audienceBarScript.Show(3, AudienceUIScript.Notice.Correct, false);
    }

    private void A5Red()
    {
        // Set variable to true
        A5RedState = true;

        // Replace alert with correct check mark
        audienceBarScript.Show(4, AudienceUIScript.Notice.Correct, true);
    }

    private void A5Blue()
    {
        // Set variable to true
        A5BlueState = true;

        // Replace alert with correct check mark
        audienceBarScript.Show(4, AudienceUIScript.Notice.Correct, false);
    }

    private void ResetRedVotingVariables()
    {
        A1RedState = false;
        A2RedState = false;
        A3RedState = false;
        A4RedState = false;
        A5RedState = false;

        for (int i = 0; i < audienceBarScript.Size(); i++) {
           audienceBarScript.Hide(i, true);
        }
    }

    private void ResetBlueVotingVariables()
    {
        A1BlueState = false;
        A2BlueState = false;
        A3BlueState = false;
        A4BlueState = false;
        A5BlueState = false;

        for (int i = 0; i < audienceBarScript.Size(); i++)
        {
            audienceBarScript.Hide(i, false);
        }
    }

    private void ResetVotingVariables()
    {
        ResetRedVotingVariables();
        ResetBlueVotingVariables();
    }
}
