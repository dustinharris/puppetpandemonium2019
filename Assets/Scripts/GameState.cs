using System.Collections;
using System.Collections.Generic;

public static class GameState
{
    public enum Game
    {
        EKO2Y, DontShootGma, RhythmLogging, None
    }

    public enum Side
    {
        Red, Blue, NA
    }

    public const int NUM_GAMES = 3;
    public static bool[] Played;
    public static Game LastSkipped;
    public static Side WhichSide;

    public static void Initialize()
    {
        Played = new bool[] { false, false, false };
        LastSkipped = Game.None;
        WhichSide = Side.NA;
    }

    public static int NumUnplayed()
    {
        int unplayed = 0;
        for (int i = 0; i < GameState.NUM_GAMES; i++)
        {
            if (!Played[i]) unplayed++;
        }
        return unplayed;
    }

    public static bool HavePlayed(Game game)
    {
        return Played[(int)game];
    }

    public static void SetPlayed(Game game)
    {
        Played[(int)game] = true;
    }

    public static Side OtherSide(Side side)
    {
        if (side == Side.Red)
        {
            return Side.Blue;
        } else if (side == Side.Blue)
        {
            return Side.Red;
        } else
        {
            return Side.NA;
        }
    }
}
