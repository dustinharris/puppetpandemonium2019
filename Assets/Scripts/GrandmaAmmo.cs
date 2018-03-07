using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaAmmo : MonoBehaviour, GameStartScript.ISubscribe
{

    private int[] Bullets;

    private const int RED = 0;
    private const int BLUE = 1;

    public const int MAG_SIZE = 5;

	void Awake () {
        Bullets = new int[] { 0, 0 };
    }

    public void CountdownFinished()
    {
        SendMessage("OutOfAmmo", true);
        SendMessage("OutOfAmmo", false);
    }

    public bool CanFire(bool red)
    {
        return Bullets[GetIndex(red)] > 0;
    }

    public void Reload(bool red)
    {
        int i = GetIndex(red);

        Bullets[i] = MAG_SIZE;

        SendMessage("Reloaded", red);
    }

    public void Fire(bool red)
    {
        if (CanFire(red))
        {
            int i = GetIndex(red);
            Bullets[i] -= 1;
            SendMessage((red ? "Red" : "Blue") + "Fired", Bullets[i]);
            if (Bullets[i] == 0)
            {
                SendMessage("OutOfAmmo", red);
            }
        }
    }

    private int GetIndex(bool red)
    {
        return red ? RED : BLUE;
    }
}
