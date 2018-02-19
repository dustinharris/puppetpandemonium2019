using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaAmmo : MonoBehaviour {

    private int[] Bullets;
    private int[] Reloaded;

    private const int RED = 0;
    private const int BLUE = 1;

    public const int MAG_SIZE = 5;

	// Use this for initialization
	void Start () {
        Bullets = new int[] { 0, 0 };
        Reloaded = new int[] { 0, 0 };

        StartCoroutine(InitialReload());
	}

    private IEnumerator InitialReload()
    {
        yield return null;
    }
	
    public bool CanFire(bool red)
    {
        return Bullets[GetIndex(red)] > 0;
    }

    public void ReloadBullet(bool red)
    {
        int i = GetIndex(red);

        if (!CanFire(red))
        {
            Reloaded[i] += 1;
            if (Reloaded[i] >= MAG_SIZE)
            {
                Reload(i);
            }
        }
    }

    private void Reload(int index)
    {
        Bullets[index] = MAG_SIZE;
        Reloaded[index] = 0;
    }

    private int GetIndex(bool red)
    {
        return red ? RED : BLUE;
    }
}
