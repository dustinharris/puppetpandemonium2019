using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaAmmo : MonoBehaviour, GameStartScript.ISubscribe
{
    public RandomPitch FireSFX;
    public RandomPitch LoadClickSFX;
    public RandomPitch ReloadSFX;

    private int[] Bullets;
    private int[] Reloading;

    private const int RED = 0;
    private const int BLUE = 1;

    public const int MAG_SIZE = 6;

	void Awake () {
        Bullets = new int[] { 0, 0 };
        Reloading = new int[] { 0, 0 };
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

    private void Reload(bool red)
    {
        int i = GetIndex(red);
        Bullets[i] = MAG_SIZE;
        Reloading[i] = 0;

        ReloadSFX.PlayRandomPitch();

        SendMessage("Reloaded", red);
    }

    // Signal in middle of reloading
    private void SendReloadingMessage(int index)
    {
        string which = index == RED ? "Red" : "Blue";
        SendMessage(which + "Reloading", Reloading[index]);
    }

    public void AddBullet(bool red)
    {
        int i = GetIndex(red);
        Reloading[i] = Mathf.Min(Reloading[i] + 1, MAG_SIZE);
        LoadClickSFX.PlayRandomPitch();

        if (Reloading[i] == MAG_SIZE)
        {
            Reload(red);
        } else
        {
            SendReloadingMessage(i);
        }
    }

    public void RemoveBullet(bool red)
    {
        int i = GetIndex(red);
        Reloading[i] = Mathf.Max(Reloading[i] - 1, 0);

        SendReloadingMessage(i);
    }

    public void Fire(bool red)
    {
        if (CanFire(red))
        {
            FireSFX.PlayRandomPitch();
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
