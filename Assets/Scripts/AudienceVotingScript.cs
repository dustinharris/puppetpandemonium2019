using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceVotingScript : MonoBehaviour {

    [SerializeField]
    private GameObject AudienceBar;
    private AudienceBarScript AudienceScript;

    private GameState.Side[] Votes;
    private int VotesRed = 0;
    private int VotesBlue = 0;

    private bool votingDisabled = false;

	// Use this for initialization
	void Start () {
        AudienceScript = AudienceBar.GetComponent<AudienceBarScript>();
        if (!votingDisabled)
        {
            AudienceScript.ShowAll(AudienceUIScript.Notice.Glow);
        }

        Votes = new GameState.Side[AudienceScript.Size()];
        for (int i = 0; i < AudienceScript.Size(); i++)
        {
            Votes[i] = GameState.Side.NA;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!votingDisabled)
        {
            // Check if each audience member has pressed buttons
            int sections = AudienceScript.Size();
            for (int i = 0; i < sections; i++)
            {
                if (Votes[i] == GameState.Side.NA)
                {
                    bool redButton = Input.GetButtonDown("Audience" + i + "Red");
                    bool blueButton = Input.GetButtonDown("Audience" + i + "Blue");
                    if (redButton && blueButton)
                    {
                        // Ignore simultaneous button presses
                        continue;
                    }
                    else if (redButton)
                    {
                        Votes[i] = GameState.Side.Red;
                        VotesRed++;
                        AudienceScript.HideGlow(i);
                        AudienceScript.Show(i, AudienceUIScript.Notice.Correct, true);
                    }
                    else if (blueButton)
                    {
                        Votes[i] = GameState.Side.Blue;
                        VotesBlue++;
                        AudienceScript.HideGlow(i);
                        AudienceScript.Show(i, AudienceUIScript.Notice.Correct, false);
                    }
                }
            }
        }
    }

    public void DisableVoting()
    {
        votingDisabled = true;
        if (AudienceScript != null) {
            AudienceScript.HideGlow();
        }
    }

    public void GetVotes(out int red, out int blue)
    {
        red = VotesRed;
        blue = VotesBlue;
    }
}
