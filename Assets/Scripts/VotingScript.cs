using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VotingScript : MonoBehaviour {

    [SerializeField]
    private GameObject RedImageObject;
    [SerializeField]
    private GameObject BlueImageObject;
    [SerializeField]
    private GameObject TimerTextObject;

    private Image RedImage;
    private Image BlueImage;
    private Text TimerText;

    private SceneSwitcher Switcher;
    private GameInfo Info;
    private AudienceVotingScript Audience;

    private GameState.Game[] Options;

	void Start () {
        RedImage = RedImageObject.GetComponent<Image>();
        BlueImage = BlueImageObject.GetComponent<Image>();
        TimerText = TimerTextObject.GetComponent<Text>();

        Switcher = GetComponent<SceneSwitcher>();
        Info = GetComponent<GameInfo>();
        Audience = GetComponent<AudienceVotingScript>();

        Options = new GameState.Game[2];

        int unplayed = GameState.NumUnplayed();
        
        GameState.Game leftover = GameState.LastSkipped;
        GameState.Side side = GameState.WhichSide;

        if (unplayed == 1)
        {
            SetOption(side, leftover);
            Options[(int)GameState.OtherSide(side)] = GameState.Game.None;

            Audience.DisableVoting();
            GameChosen(side);
        } else {
            if (unplayed == 2)
            {
                GameState.Game otherGame = GetOther(leftover);
                GameState.Side otherSide = GameState.OtherSide(side);
                SetOption(side, leftover);
                SetOption(otherSide, otherGame);
            } else
            {
                // Randomly choose between unplayed games
                GameState.Game first = (GameState.Game)Random.Range(0, GameState.NUM_GAMES);
                GameState.Game second;
                while ((second = (GameState.Game)Random.Range(0, GameState.NUM_GAMES)) == first) {; }

                SetOption(GameState.Side.Red, first);
                SetOption(GameState.Side.Blue, second); 
            }

            // Wait for voting to come in
            StartCoroutine(Countdown());
            StartCoroutine(CheckVotes());
        }
	}

    private void GameChosen(GameState.Side side)
    {
        TimerText.enabled = false;
        GameState.Game chosen = Options[(int)side];
        GameState.Game skipped = Options[(int)GameState.OtherSide(side)];

        // Update game state
        GameState.LastSkipped = skipped;
        GameState.WhichSide = GameState.OtherSide(side);
        GameState.SetPlayed(chosen);

        Switcher.SetNextScene(Info.GetSceneName(chosen));

        StartCoroutine(DisplayWinner(chosen));
    }

    private void Timeout()
    {
        int red;
        int blue;
        Audience.GetVotes(out red, out blue);
        GameChosen(DetermineWinner(red, blue));
    }

    private IEnumerator Countdown()
    {
        for (int time = 30; time > 0; time--)
        {
            TimerText.text = time.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        TimerText.text = "0";
        Timeout();
    }

    private IEnumerator CheckVotes()
    {
        while(true)
        {
            int red; int blue;
            Audience.GetVotes(out red, out blue);
            if (red + blue == 5)
            {
                Debug.Log("All votes counted");
                GameChosen(DetermineWinner(red, blue));
                yield break;
            } else
            {
                yield return null;
            }

        }
    }

    // Show chosen game for a few seconds, which also gives new scene time to load
    private IEnumerator DisplayWinner(GameState.Game winner)
    {
        // TODO show winner prominently
        yield return new WaitForSeconds(3.0f);

        Switcher.SwitchScenes();
    }

    private GameState.Side DetermineWinner(int redVotes, int blueVotes)
    {
        if (redVotes > blueVotes)
        {
            return GameState.Side.Red;
        }
        else if (redVotes < blueVotes)
        {
            return GameState.Side.Blue;
        }
        else
        {
            return (GameState.Side)Random.Range(0, 1);
        }
    }

    private void SetOption(GameState.Side side, GameState.Game game)
    {
        Sprite sprite = Info.GetSprite(game);
        Image img = GetImage(side);

        img.sprite = sprite;
        Options[(int)side] = game;
        Debug.Log(side.ToString() + ": " + game.ToString());
    }

    private Image GetImage(GameState.Side side)
    {
        if (side == GameState.Side.Blue)
        {
            return BlueImage;
        } else
        {
            return RedImage;
        }
    }

    // If there are two games remaining, gets the other unplayed one
    private GameState.Game GetOther(GameState.Game leftover)
    {
        for (int i = 0; i < GameState.NUM_GAMES; i++)
        {
            if (!GameState.Played[i] && (i != (int)leftover))
            {
                return (GameState.Game)i;
            }
        }

        return GameState.Game.None;
    }
}
