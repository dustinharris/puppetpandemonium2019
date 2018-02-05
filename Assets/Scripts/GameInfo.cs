using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo: MonoBehaviour {
    
    [SerializeField]
    private Sprite EKO2YLogo;
    [SerializeField]
    private Sprite DontShootGmaLogo;
    [SerializeField]
    private Sprite RhythmLoggingLogo;

    [SerializeField]
    private string EKO2YScene = "_Game_EKO2Y";
    [SerializeField]
    private string DontShootGmaScene = "_Game_DontShootGrandma";
    [SerializeField]
    private string RhythmLoggingScene = "_Game_RhythmLogging";

    private Sprite[] Sprites;

    void Awake() {
        Sprites = new Sprite[GameState.NUM_GAMES];
        Sprites[(int)GameState.Game.EKO2Y] = EKO2YLogo;
        Sprites[(int)GameState.Game.DontShootGma] = DontShootGmaLogo;
        Sprites[(int)GameState.Game.RhythmLogging] = RhythmLoggingLogo;
	}
    
    public Sprite GetSprite(GameState.Game game)
    {
        if (game != GameState.Game.None)
        {
            return Sprites[(int)game];
        } else
        {
            Debug.Log("No game specified for sprite");
            return null;
        }
    }

    public string GetSceneName(GameState.Game game)
    {
        switch (game)
        {
            case GameState.Game.EKO2Y:
                return EKO2YScene;
            case GameState.Game.DontShootGma:
                return DontShootGmaScene;
            case GameState.Game.RhythmLogging:
                return RhythmLoggingScene;
            default:
                return null;
        }
    }
}
