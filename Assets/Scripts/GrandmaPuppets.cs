using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaPuppets : MonoBehaviour
{
    private GrandmaAmmo ammo;
    private bool RedReloading = false;
    private bool BlueReloading = false;

    private bool started = false;

    void Awake()
    {
        ammo = GetComponent<GrandmaAmmo>();
    }

    // Sent from ammo script on game start
    public void OutOfAmmo(bool red)
    {
        started = true;
    }

    void Update()
    {
        if (!started)
        {
            return;
        }
        if (Input.GetButtonDown("RedPuppet"))
        {
            if (ammo.CanFire(true))
            {
                ammo.Fire(true);
            } else
            {
                if (!RedReloading)
                {
                    RedReloading = true;
                    ammo.AddBullet(true);
                }
            }  
        }
        if (Input.GetButtonUp("RedPuppet"))
        {
            if (!ammo.CanFire(true) && RedReloading)
            {
                RedReloading = false;
                ammo.RemoveBullet(true);
            }
        }
        if (Input.GetButtonDown("BluePuppet"))
        {
            if (ammo.CanFire(false))
            {
                ammo.Fire(false);
            } else
            {
                if (!BlueReloading)
                {
                    BlueReloading = true;
                    ammo.AddBullet(false);
                }
            }
        }
        if (Input.GetButtonUp("BluePuppet"))
        {
            if (!ammo.CanFire(false) && BlueReloading)
            {
                BlueReloading = false;
                ammo.RemoveBullet(false);
            }
        }
    }
}
