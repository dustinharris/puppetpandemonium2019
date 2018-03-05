using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EKO2YVoting : MonoBehaviour {

    [SerializeField] private AudienceBarScript audienceBarScript;
    private bool[] StateRed;
    private bool[] StateBlue;
    private int size;
    private bool redInVotingState;
    private bool blueInVotingState;

    [SerializeField] private GameObject HonkIconRed;
    [SerializeField] private GameObject HonkIconBlue;
    [SerializeField] private HonkController Honker;

    private AudienceHeartController HeartController;

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

        HeartController = GetComponent<AudienceHeartController>();
    }
    
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.P1_ALL_BLUE, P1AllBlue);
        Messenger.RemoveListener(GameEvent.P2_ALL_RED, P2AllRed);
    }

    // Use this for initialization
    void Start () {
        // Initialize audience button state booleans
        size = audienceBarScript.Size();

        StateRed = new bool[size];
        StateBlue = new bool[size];

        for (int i = 0; i < size; i++)
        {
            StateRed[i] = false;
            StateBlue[i] = false;
        }

        // Initialize voting state booleans
        redInVotingState = false;
        blueInVotingState = false;

        // Make buttons glow
        audienceBarScript.ShowAll(AudienceUIScript.Notice.Glow);
    }

    private void Update()
    {
        // Check to see if blue character is currently in voting state
        if (blueInVotingState)
        {
            // If all audience members have voted for blue, release holding state
            if (AllPressed(false))
            {
                // Broadcast Message to release P2
                Messenger.Broadcast(GameEvent.P1_RELEASE);

                // Set all audience variables to false
                ResetVotingVariables(false);

                // Take blue out of voting state
                blueInVotingState = false;
            }
        }
        // Check to see if red character is currently in voting state
        if (redInVotingState)
        {
            // If all audience members have voted for red, release holding state
            if (AllPressed(true))
            {
                // Broadcast Message to release P1
                Messenger.Broadcast(GameEvent.P2_RELEASE);

                // Set all audience variables to false
                ResetVotingVariables(true);

                // Take red out of voting state
                redInVotingState = false;
            }
        } 
    }

    private bool AllPressed(bool red)
    {
        bool[] state = GetStates(red);

        for (int i = 0; i < size; i++)
        {
            if (!state[i])
            {
                return false;
            }
        }

        return true;
    }

    private bool[] GetStates(bool red)
    {
        return red ? StateRed : StateBlue;
    }

    public void Honk(bool red)
    {
        StartCoroutine(ShowHonk(red));
        Honker.Honk();
    }

    private IEnumerator ShowHonk(bool red)
    {
        GameObject honk = red ? HonkIconRed : HonkIconBlue;

        honk.SetActive(true);
        yield return new WaitForSeconds(.1f);
        honk.SetActive(false);
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

    private void AudienceInput(bool red, int index)
    {
        if (InVotingState(red))
        {
            bool[] states = GetStates(red);

            if (!states[index])
            {
                states[index] = true;

                // Replace alert with correct check mark
                audienceBarScript.Show(index, AudienceUIScript.Notice.Correct, red);
                HeartController.ShowHeart(red, index);
            }
        }
        else
        {
            Honk(red);
        }
    }

    private bool InVotingState(bool red)
    {
        return red ? redInVotingState : blueInVotingState;
    }

    private void ResetVotingVariables(bool red)
    {
        bool[] states = GetStates(red);

        for (int i = 0; i < size; i++)
        {
            states[i] = false;

            audienceBarScript.Hide(i, red);
        }
    }

    private void ResetVotingVariables()
    {
        ResetVotingVariables(true);
        ResetVotingVariables(false);
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
}
